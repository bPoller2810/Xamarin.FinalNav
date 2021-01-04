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
        public ContentPage GetInstance(IReadOnlyList<ServiceRegistrationContainer> services, params NavigationParameter[] userParameters)
        {
            var page = GetPage(services, userParameters?.Where(p => p.Type == EParameterType.Page).ToArray());
            var vm = GetVMInstance(services, userParameters?.Where(p => p.Type == EParameterType.ViewModel).ToArray());

            page.BindingContext = vm;
            return page;
        }

        #endregion

        private INotifyPropertyChanged GetVMInstance(IReadOnlyList<ServiceRegistrationContainer> services, params NavigationParameter[] userParameters)
        {
            var constructor = DIHelper.GetBestMatchingConstructor(VmType, services, userParameters);
            var instance = constructor.CreateObject();
            if (instance is not INotifyPropertyChanged notifyPropertyChanged)
            {
                throw new InvalidCastException($"{VmType} has to implement INotifyPropertyChanged");
            }
            return notifyPropertyChanged;
        }

        private ContentPage GetPage(IReadOnlyList<ServiceRegistrationContainer> services, params NavigationParameter[] userParameters)
        {
            var constructor = DIHelper.GetBestMatchingConstructor(PageType, services, userParameters);
            var instance = constructor.CreateObject();
            if (instance is not ContentPage page)
            {
                throw new InvalidCastException($"{PageType} must extend ContentPage");
            }
            return page;
        }

    }
}
