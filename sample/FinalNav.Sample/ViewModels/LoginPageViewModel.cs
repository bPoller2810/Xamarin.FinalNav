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

        #region private member
        private readonly INavigationService _navigationService;
        #endregion

        #region properties
        private string _username;
        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }

        public ICommand LoginCommand { get; private set; }
        #endregion

        #region ctor
        public LoginPageViewModel(INavigationService navigationService)
        {
            LoginCommand = new AsyncCommand(ExecuteAsyncCommand);
            _navigationService = navigationService;
        }
        #endregion

        #region Command handling
        private async Task ExecuteAsyncCommand()
        {
            await _navigationService.PushAsync<UserPage>(new NavigationParameter
            {
                Type = EParameterType.ViewModel,
                Parameter = Username,
            });
        }
        #endregion

    }
}
