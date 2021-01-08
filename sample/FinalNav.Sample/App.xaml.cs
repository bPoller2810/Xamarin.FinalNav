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
        public App(FinalIoc container)
        {
            InitializeComponent();


            container.RegisterService<IDemoService, DefaultDemoService>();

            container.RegisterPage<LoginPage, LoginPageViewModel>();
            container.RegisterPage<UserPage, UserPageViewModel>();

            new FinalNavigator(this, container).Build<LoginPage>();
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
