using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.FinalNav.DI;
using Xamarin.Forms;

namespace Xamarin.FinalNav.Containers
{
    internal class PageRegistrationContainer
    {

        #region properties
        public Type PageType { get; init; }
        public Type VmType { get; init; }
        #endregion

        #region public methods
        public Page GetInstance(IReadOnlyList<ServiceRegistrationContainer> services, params NavigationParameter[] userParameters)
        {
            var page = GetPage(services, userParameters?.Where(p => p.Type == EParameterType.Page).ToArray());
            var vm = GetVMInstance(services, userParameters?.Where(p => p.Type == EParameterType.ViewModel).ToArray());

            page.BindingContext = vm;
            return page;
        }
        #endregion

        #region private helper
        private INotifyPropertyChanged GetVMInstance(IReadOnlyList<ServiceRegistrationContainer> services, params NavigationParameter[] userParameters)
        {
            var constructor = DIHelper.GetBestMatchingConstructor(VmType, services, userParameters);
            var instance = constructor.CreateObject();

            return (INotifyPropertyChanged)instance;
        }

        private Page GetPage(IReadOnlyList<ServiceRegistrationContainer> services, params NavigationParameter[] userParameters)
        {
            var constructor = DIHelper.GetBestMatchingConstructor(PageType, services, userParameters);
            var instance = constructor.CreateObject();
            if (instance is not Page page)
            {
                throw new InvalidCastException($"{PageType} must extend ContentPage");
            }
            return page;
        }
        #endregion

    }
}
