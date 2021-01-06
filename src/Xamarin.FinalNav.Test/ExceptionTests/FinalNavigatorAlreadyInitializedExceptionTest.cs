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

    [TestFixture]
    public class FinalNavigatorAlreadyInitializedExceptionTest
    {


        [SetUp]
        public void Init()
        {
            MockXF.Init();

            FinalNavigator.Instance.CleanSystem();
            FinalNavigator.Instance.RegisterPage<TestPage, TestViewModel>();
            FinalNavigator.Instance.InitializeRoot<TestPage>(new Application());
        }

        [Test]
        public void ThrowsOnRegisterService()
        {
            Assert.Throws<InvalidOperationException>(() => FinalNavigator.Instance.RegisterService<ITestService, TestService>());
        }

        [Test]
        public void ThrowsOnRegisterPage()
        {
            Assert.Throws<InvalidOperationException>(() => FinalNavigator.Instance.RegisterPage<TestPage, TestViewModel>());
        }

       

    }
}
