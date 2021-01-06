using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.FinalNav.Test.Models.TestPages;
using Xamarin.FinalNav.Test.Models.TestServices;
using Xamarin.FinalNav.Test.XFMock;
using Xamarin.Forms;

namespace Xamarin.FinalNav.Test
{
    public class FinalNavigatorRegistrationExceptionTest
    {

        [SetUp]
        public void Init()
        {
            MockXF.Init();

            FinalNavigator.Instance.CleanSystem();
            FinalNavigator.Instance.RegisterPage<TestPage, TestViewModel>();
            FinalNavigator.Instance.RegisterService<ITestService, TestService>();
        }

        [Test]
        public void ThrowsOnPushAsyncMissingPageType()
        {
            FinalNavigator.Instance.InitializeRoot<TestPage>(new Application());
            Assert.ThrowsAsync<ArgumentException>(() => FinalNavigator.Instance.PushAsync<NotRegisteredPage>());
        }

        [Test]
        public void ThrowsOnPushModelAsyncMissingPageType()
        {
            FinalNavigator.Instance.InitializeRoot<TestPage>(new Application());
            Assert.ThrowsAsync<ArgumentException>(() => FinalNavigator.Instance.PushModalAsync<NotRegisteredPage>());
        }

        [Test]
        public void ThrowsOnRegisterPageWithAlreadyRegisteredCombination()
        {
            Assert.Throws<ArgumentException>(() => FinalNavigator.Instance.RegisterPage<TestPage, TestViewModel>());
        }

        [Test]
        public void ThrowsOnRegisterServiceWithAlreadyRegisteredCombination()
        {
            Assert.Throws<ArgumentException>(() => FinalNavigator.Instance.RegisterService<ITestService, TestService>());
        }

    }
}
