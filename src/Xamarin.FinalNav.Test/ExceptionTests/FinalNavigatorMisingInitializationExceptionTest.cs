using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.FinalNav.DI;
using Xamarin.FinalNav.Test.Models.TestPages;
using Xamarin.FinalNav.Test.Models.TestServices;
using Xamarin.FinalNav.Test.XFMock;
using Xamarin.Forms;

namespace Xamarin.FinalNav.Test
{

    [TestFixture]
    public class FinalNavigatorMisingInitializationExceptionTest
    {

        [SetUp]
        public void Init()
        {
            FinalNavigator.Instance.CleanSystem();
        }

        [Test]
        public void ThrowsOnNavigationGetter()
        {
            Assert.Throws<InvalidOperationException>(() => _ = FinalNavigator.Instance.Navigation);
        }

        [Test]
        public void ThrowsOnNavigationStackGetter()
        {
            Assert.Throws<InvalidOperationException>(() => _ = FinalNavigator.Instance.NavigationStack);
        }

        [Test]
        public void ThrowsOnPushAsync()
        {
            Assert.ThrowsAsync<InvalidOperationException>(() => FinalNavigator.Instance.PushAsync<TestPage>());
        }

        [Test]
        public void ThrowsOnPopAsync()
        {
            Assert.ThrowsAsync<InvalidOperationException>(() => FinalNavigator.Instance.PopAsync());
        }

        [Test]
        public void ThrowsOnPopToRootAsync()
        {
            Assert.ThrowsAsync<InvalidOperationException>(() => FinalNavigator.Instance.PopToRootAsync());
        }

        [Test]
        public void ThrowsOnPushModalAsync()
        {
            Assert.ThrowsAsync<InvalidOperationException>(() => FinalNavigator.Instance.PushModalAsync<TestPage>());
        }


        [Test]
        public void ThrowsOnPopModalAsync()
        {
            Assert.ThrowsAsync<InvalidOperationException>(() => FinalNavigator.Instance.PopModalAsync());
        }


        

    }
}
