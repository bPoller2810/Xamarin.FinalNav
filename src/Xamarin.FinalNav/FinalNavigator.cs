using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.FinalNav.Containers;
using Xamarin.Forms;

namespace Xamarin.FinalNav
{
    public sealed class FinalNavigator
    {

        #region threadsafe singleton
        private static readonly object _lock = new object();
        private static FinalNavigator _instance;
        private FinalNavigator()
        {
            _services = new List<ServiceRegistrationContainer>();
            _pages = new List<PageRegistrationContainer>();
        }
        public static FinalNavigator Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance is null)
                    {
                        _instance = new FinalNavigator();
                    }
                    return _instance;
                }
            }
        }
        #endregion

        #region locks
        private static readonly object _initLock = new object();
        #endregion

        #region system
        private bool _initialized;
        private Application _app;
        private INavigation _navigation;

        public void InitializeRoot<TRootPage>(Application app)
            where TRootPage : ContentPage
        {
            lock (_initLock)
            {
                if (_initialized)
                {
                    throw new InvalidOperationException("FinalNavigator is already initialized");
                }
                if (_pages.Count == 0)
                {
                    throw new InvalidOperationException("Register your pages first");
                }
                if (app is null)
                {
                    throw new ArgumentNullException(nameof(app));
                }
                _app = app;

                var page = GetPage<TRootPage>();
                _app.MainPage = new NavigationPage(page);
                _navigation = _app.MainPage.Navigation;
                _initialized = true;
            }
        }

        public void CleanSystem()
        {
            lock (_initLock)
            {
                _app = null;
                _navigation = null;
                _services.Clear();
                _pages.Clear();
                _initialized = false;
            }
        }
        #endregion

        #region services
        private readonly List<ServiceRegistrationContainer> _services;

        public void RegisterService<TServiceType, TServiceImplementation>(EServiceLifetime lifetime = EServiceLifetime.Singleton)
            where TServiceType : class
            where TServiceImplementation : class, TServiceType
        {
            if (_initialized)
            {
                throw new InvalidOperationException("FinalNavigator is already initialized");
            }
            var serviceType = typeof(TServiceType);
            if (!serviceType.IsInterface) { throw new NotSupportedException($"{serviceType.Name} must be an Interface"); }

            var implementationType = typeof(TServiceImplementation);

            if (_services.Any(s => s.ServiceType.Equals(serviceType) && s.ServiceImplementation.Equals(implementationType)))
            {
                throw new ArgumentException("Interface <-> Implementation combination already registered");
            }

            _services.Add(new ServiceRegistrationContainer
            {
                ServiceType = serviceType,
                ServiceImplementation = implementationType,
                Lifetime = lifetime,
            });
        }
        #endregion

        #region pages
        private readonly List<PageRegistrationContainer> _pages;

        public void RegisterPage<TPage, TVm>()
            where TPage : ContentPage
            where TVm : INotifyPropertyChanged
        {
            if (_initialized)
            {
                throw new InvalidOperationException("FinalNavigator is already initialized");
            }
            var pageType = typeof(TPage);
            var vmType = typeof(TVm);

            if (_pages.Any(p => p.PageType.Equals(pageType) && p.VmType.Equals(vmType)))
            {
                throw new ArgumentException("Page <-> ViewModel combination already registered");
            }

            _pages.Add(new PageRegistrationContainer
            {
                PageType = pageType,
                VmType = vmType,
            });
        }

        private TPage GetPage<TPage>(params NavigationParameter[] userParameters)
            where TPage : ContentPage
        {
            var pageType = typeof(TPage);

            var container = _pages.FirstOrDefault(p => p.PageType.Equals(pageType));
            if (container is null) { throw new ArgumentException($"Page of Type {pageType.Name} not found"); }

            return (TPage)container.GetInstance(_services, userParameters);
        }
        #endregion

        #region navigation

        #region management
        public INavigation Navigation => _navigation;
        public IReadOnlyList<NavigationItem> NavigationStack
        {
            get
            {
                if (!_initialized)
                {
                    throw new InvalidOperationException("FinalNavigator is not initialized");
                }
                return _navigation.NavigationStack.Select(s => new NavigationItem
                {
                    PageType = s.GetType(),
                }).ToList();
            }
        }
        #endregion

        #region stack
        public async Task PushAsync<TPage>(params NavigationParameter[] userParameters)
            where TPage : ContentPage
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            var page = GetPage<TPage>(userParameters);
            if (page is null)
            {
                throw new InvalidOperationException($"{typeof(TPage).Name} is not registered");
            }
            await _navigation.PushAsync(page);
        }
        public async Task PopAsync()
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            await _navigation.PopAsync();
        }
        public async Task PopToRootAsync()
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            await _navigation.PopToRootAsync();
        }
        #endregion

        #region modal stack
        public async Task PushModalAsync<TPage>()
            where TPage : ContentPage
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            var page = GetPage<TPage>();
            if (page is null)
            {
                throw new InvalidOperationException($"{typeof(TPage).Name} is not registered");
            }
            await _navigation.PushModalAsync(page);
        }
        public async Task PopModalAsync()
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            await _navigation.PopModalAsync();
        }
        #endregion

        #endregion
    }

}
