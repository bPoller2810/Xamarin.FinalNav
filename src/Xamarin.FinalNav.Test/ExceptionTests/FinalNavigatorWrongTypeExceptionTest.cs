using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.FinalNav.Test.Models.TestPages;
using Xamarin.FinalNav.Test.Models.TestServices;
using Xamarin.FinalNav.Test.XFMock;

namespace Xamarin.FinalNav.Test
{
    public class FinalNavigatorWrongTypeExceptionTest
    {

        [SetUp]
        public void Init()
        {
            MockXF.Init();

            FinalNavigator.Instance.CleanSystem();
        }

        [Test]
        public void ThrowsOnRegisterServiceWithoutInterface()
        {
            Assert.Throws<NotSupportedException>(() => FinalNavigator.Instance.RegisterService<TestService, TestService>());
        }

    }
}
