using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.FinalNav.Test.Models.TestPages;
using Xamarin.FinalNav.Test.XFMock;

namespace Xamarin.FinalNav.Test
{
    public class FinalNavigatorMissingArgumentExceptionTest
    {

        [SetUp]
        public void Init()
        {
            MockXF.Init();

            FinalNavigator.Instance.CleanSystem();
            FinalNavigator.Instance.RegisterPage<TestPage, TestViewModel>();
        }

        [Test]
        public void ThrowsOnMissingPages()
        {
            Assert.Throws<ArgumentNullException>(() => FinalNavigator.Instance.InitializeRoot<TestPage>(null));
        }

    }
}
