using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.FinalNav
{
    public interface INavigationService
    {

        Task PushAsync<TPage>(params NavigationParameter[] userParameters) where TPage : Page;
        Task PushAsync<TPage, TViewModel>(params NavigationParameter[] userParameters) where TPage : Page where TViewModel : INotifyPropertyChanged;
        Task PopAsync();
        Task PopToRootAsync();

        Task PushModalAsync<TPage>(params NavigationParameter[] userParameters) where TPage : Page;
        Task PushModalAsync<TPage, TViewModel>(params NavigationParameter[] userParameters) where TPage : Page where TViewModel : INotifyPropertyChanged;
        Task PopModalAsync();

    }
}
