using FinalNav.Sample.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.FinalCtrl.Helper;
using Xamarin.FinalNav;

namespace FinalNav.Sample.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {


        private string _username;
        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }


        public ICommand LoginCommand { get; private set; }

        public LoginPageViewModel()
        {
            LoginCommand = new AsyncCommand(ExecuteAsyncCommand);
        }

        private async Task ExecuteAsyncCommand()
        {
            await FinalNavigator.Instance.PushAsync<UserPage>(new NavigationParameter
            {
                Type = EParameterType.ViewModel,
                Parameter = Username,
            });
        }

    }
}
