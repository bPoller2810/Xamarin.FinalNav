using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.FinalNav
{
    public interface INavigationService
    {

        Task PushAsync<TPage>(params NavigationParameter[] userParameters) where TPage : Page;
        Task PopAsync();
        Task PopToRootAsync();

        Task PushModalAsync<TPage>(params NavigationParameter[] userParameters) where TPage : Page;
        Task PopModalAsync();

    }
}
