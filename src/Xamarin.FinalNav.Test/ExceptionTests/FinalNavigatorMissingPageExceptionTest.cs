using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.FinalNav.Test.Models.TestPages;
using Xamarin.FinalNav.Test.XFMock;
using Xamarin.Forms;

namespace Xamarin.FinalNav.Test
{
    public class FinalNavigatorMissingPageExceptionTest
    {

        [SetUp]
        public void Init()
        {
            MockXF.Init();

            FinalNavigator.Instance.CleanSystem();
        }

        [Test]
        public void ThrowsOnMissingPages()
        {
            Assert.Throws<InvalidOperationException>(() => FinalNavigator.Instance.InitializeRoot<TestPage>(new Application()));
        }

    }
}
