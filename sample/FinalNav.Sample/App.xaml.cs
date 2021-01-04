using FinalNav.Sample.Pages;
using FinalNav.Sample.Services;
using FinalNav.Sample.ViewModels;
using System;
using Xamarin.FinalNav;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalNav.Sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            FinalNavigator.Instance.RegisterService<IDemoService, DefaultDemoService>();

            FinalNavigator.Instance.RegisterPage<LoginPage, LoginPageViewModel>();
            FinalNavigator.Instance.RegisterPage<UserPage, UserPageViewModel>();

            FinalNavigator.Instance.InitializeRoot<LoginPage>(this);

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
