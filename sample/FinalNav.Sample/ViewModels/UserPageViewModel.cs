using FinalNav.Sample.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.FinalCtrl.Helper;

namespace FinalNav.Sample.ViewModels
{
    public class UserPageViewModel : BaseViewModel
    {

        #region properties
        private string _username;
        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }
        #endregion

        #region ctor
        public UserPageViewModel()
        {//false
        }
        public UserPageViewModel(string username)
        {//true 1
            Username = username;
        }
        public UserPageViewModel(bool test, IDemoService service, string username, IPlatformDependentService plat)
        {//false 3
            Username = username;
        }
        public UserPageViewModel(IDemoService service, string username, IPlatformDependentService plat)
        {//true 3 <- the picked one (satisfies all user parameters, doesnt leave any parameters unfilled and can fill the most services)
            Username = username;
        }
        public UserPageViewModel(bool test, IDemoService service, IPlatformDependentService plat)
        {//false 2
        }
        public UserPageViewModel(bool test, INotifyPropertyChanged prop, string username, IDemoService service)
        {//false 3
            Username = username;
        }
        public UserPageViewModel(bool test, INotifyPropertyChanged prop, string username)
        {//false 2
            Username = username;
        }
        public UserPageViewModel(string username, bool Test)
        {//false 1
            Username = username;
        }
        #endregion

    }
}
