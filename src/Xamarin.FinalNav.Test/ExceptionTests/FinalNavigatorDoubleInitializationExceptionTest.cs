using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.FinalNav.Test.Models.TestPages;
using Xamarin.FinalNav.Test.XFMock;
using Xamarin.Forms;

namespace Xamarin.FinalNav.Test
{
    public class FinalNavigatorDoubleInitializationExceptionTest
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
        public void ThrowsOnDoubleInitialization()
        {
            Assert.Throws<InvalidOperationException>(() => FinalNavigator.Instance.InitializeRoot<TestPage>(new Application()));
        }

    }
}
