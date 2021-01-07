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
    public sealed class FinalNavigator : INavigationService
    {

        #region private member
        public bool Initialized { get; private set; }
        private readonly Application _app;
        private INavigation _navigation;
        private readonly FinalIoc _iocContainer;
        #endregion

        #region ctor
        public FinalNavigator(Application app, FinalIoc iocContainer)
        {
            _iocContainer = iocContainer;
            if (!_iocContainer.IsRegistered<INavigationService>())
            {
                _iocContainer.RegisterService<INavigationService>(this);
            }
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            _app = app;
        }
        #endregion

        #region public init
        public void Build<TRootPage>()
            where TRootPage : Page
        {
            var page = _iocContainer.GetPage<TRootPage>();
            _app.MainPage = new NavigationPage(page);
            _navigation = _app.MainPage.Navigation;
            Initialized = true;
        }
        #endregion

        #region stack
        public async Task PushAsync<TPage>(params NavigationParameter[] userParameters)
            where TPage : Page
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            var page = _iocContainer.GetPage<TPage>(userParameters);
            await _navigation.PushAsync(page);
        }
        public async Task PopAsync()
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            await _navigation.PopAsync();
        }
        public async Task PopToRootAsync()
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            await _navigation.PopToRootAsync();
        }
        #endregion

        #region modal stack
        public async Task PushModalAsync<TPage>(params NavigationParameter[] userParameters)
            where TPage : Page
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            var page = _iocContainer.GetPage<TPage>(userParameters);
            await _navigation.PushModalAsync(page);
        }
        public async Task PopModalAsync()
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            await _navigation.PopModalAsync();
        }
        #endregion

    }

}
