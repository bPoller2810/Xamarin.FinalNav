using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.FinalNav.Test.Models.TestPages;
using Xamarin.FinalNav.Test.XFMock;
using Xamarin.Forms;

namespace Xamarin.FinalNav.Test
{
    public class FinalNavigatorInitializationTest
    {

        [SetUp]
        public void Init()
        {
            MockXF.Init();

            FinalNavigator.Instance.CleanSystem();
            FinalNavigator.Instance.RegisterPage<TestPage, TestViewModel>();
        }

        [Test]
        public void Initializing()
        {
            Assert.IsFalse(FinalNavigator.Instance.Initialized);
            FinalNavigator.Instance.InitializeRoot<TestPage>(new Application());
            Assert.IsTrue(FinalNavigator.Instance.Initialized);
        }

        [Test]
        public void Cleanup()
        {
            FinalNavigator.Instance.InitializeRoot<TestPage>(new Application());
            Assert.IsTrue(FinalNavigator.Instance.Initialized);
            FinalNavigator.Instance.CleanSystem();
            Assert.IsFalse(FinalNavigator.Instance.Initialized);
        }

        [Test]
        public void NavigationReferencesApplicationMainPageNavigation()
        {
            var app = new Application();
            FinalNavigator.Instance.InitializeRoot<TestPage>(app);
            Assert.AreSame(FinalNavigator.Instance.Navigation, app.MainPage.Navigation);
        }

        [Test]
        public void ThrowsOnNavigationGetterAfterCleanup()
        {
            FinalNavigator.Instance.InitializeRoot<TestPage>(new Application());
            FinalNavigator.Instance.CleanSystem();
            Assert.Throws<InvalidOperationException>(() => _ = FinalNavigator.Instance.Navigation);
        }

        [Test]
        public void ThrowsOnNavigationStackGetterAfterCleanup()
        {
            FinalNavigator.Instance.InitializeRoot<TestPage>(new Application());
            FinalNavigator.Instance.CleanSystem();
            Assert.Throws<InvalidOperationException>(() => _ = FinalNavigator.Instance.NavigationStack);
        }

    }
}
