using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.FinalNav.Containers;
using Xamarin.Forms;

namespace Xamarin.FinalNav
{
    public class FinalIoc
    {

        #region private member
        private readonly List<ServiceRegistrationContainer> _services;
        private readonly List<PageRegistrationContainer> _pages;
        #endregion

        public FinalIoc()
        {
            _services = new List<ServiceRegistrationContainer>();
            _pages = new List<PageRegistrationContainer>();
        }

        #region registrationCheck
        public bool IsRegistered<TType>()
            where TType : class
        {
            var type = typeof(TType);

            if (_services.Any(s => s.ServiceType.Equals(type))) { return true; }
            if (_pages.All(p => p.PageType.Equals(type))) { return true; }

            return false;
        }
        #endregion

        #region registration
        public void RegisterService<TServiceType, TServiceImplementation>(EServiceLifetime lifetime = EServiceLifetime.Singleton)
            where TServiceType : class
            where TServiceImplementation : class, TServiceType
        {
            var serviceType = typeof(TServiceType);
            if (!serviceType.IsInterface)
            {
                throw new NotSupportedException($"{serviceType.Name} must be an Interface");
            }
            if (_services.Any(s => s.ServiceType.Equals(serviceType)))
            {
                throw new ArgumentException("Service already registered");
            }

            var implementationType = typeof(TServiceImplementation);
            _services.Add(new ServiceRegistrationContainer
            {
                ServiceType = serviceType,
                ServiceImplementation = implementationType,
                Lifetime = lifetime,
            });
        }
        public void RegisterService<TServiceType>(TServiceType instance)
            where TServiceType : class
        {
            var serviceType = typeof(TServiceType);
            if (!serviceType.IsInterface)
            {
                throw new NotSupportedException($"{serviceType.Name} must be an Interface");
            }
            if (_services.Any(s => s.ServiceType.Equals(serviceType)))
            {
                throw new ArgumentException("Service already registered");
            }

            _services.Add(new ServiceRegistrationContainer(instance)
            {
                ServiceType = serviceType,
            });
        }

        public void RegisterPage<TPage, TVm>()
            where TPage : Page
            where TVm : INotifyPropertyChanged
        {
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
        #endregion

        #region remove
        public void RemoveService<TServiceType>()
            where TServiceType : class
        {
            if (typeof(TServiceType) is INavigationService) { return; }

            var serviceType = typeof(TServiceType);
            _services.RemoveAll(s => s.ServiceType.Equals(serviceType));
        }
        public void RemovePage<TPage>()
            where TPage : Page
        {
            var pageType = typeof(TPage);
            _pages.RemoveAll(p => p.PageType.Equals(pageType));
        }
        #endregion

        #region getter
        public TPage GetPage<TPage>(params NavigationParameter[] userParameters)
            where TPage : Page
        {
            var pageType = typeof(TPage);

            var container = _pages.FirstOrDefault(p => p.PageType.Equals(pageType));
            if (container is null)
            {
                throw new ArgumentException($"Page of Type {pageType.Name} not found");
            }

            return (TPage)container.GetInstance(_services, userParameters);
        }
        public TServiceType GetService<TServiceType>()
            where TServiceType : class
        {
            var serviceType = typeof(TServiceType);

            var service = _services.FirstOrDefault(s => s.ServiceType.Equals(serviceType));
            if (service is null)
            {
                throw new ArgumentException($"Serivce of Type {serviceType.Name} not found");
            }

            return (TServiceType)service.GetInstance(_services);
        }
        #endregion

    }
}
