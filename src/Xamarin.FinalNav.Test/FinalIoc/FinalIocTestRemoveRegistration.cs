using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.FinalNav.Test.Models.TestPages;
using Xamarin.FinalNav.Test.Models.TestServices;
using Xamarin.FinalNav.Test.XFMock;
using Xamarin.Forms;

namespace Xamarin.FinalNav.Test
{
    [TestFixture]
    public class FinalIocTestRemoveRegistration
    {

        private FinalIoc _ioc;

        [SetUp]
        public void InitTest()
        {
            MockXF.Init();

            _ioc = new FinalIoc();

            _ioc.RegisterService<INavigationService, FinalNavigator>();

            _ioc.RegisterService<ITestService, TestService>();
            _ioc.RegisterService<ITest2Service, Test2Service>();

            _ioc.RegisterPage<TestPage, TestViewModel>();
            _ioc.RegisterPage<Test2Page, Test2ViewModel>();
            _ioc.RegisterPage<Test2Page, TestViewModel>();
        }

        [Test]
        public void TestRemoveService()
        {
            Assert.IsTrue(_ioc.RemoveService<ITestService>());
            Assert.AreEqual(2, _ioc._services.Count);
            Assert.IsFalse(_ioc._services.Any(s => s.ServiceType.Equals(typeof(ITestService))));
        }

        [Test]
        public void TestRemoveServiceNotregistered()
        {
            Assert.IsFalse(_ioc.RemoveService<ITest3Service>());
            Assert.AreEqual(3, _ioc._services.Count);
        }

        [Test]
        public void TestRemoveSerivceINavigationService()
        {
            Assert.IsFalse(_ioc.RemoveService<INavigationService>());
            Assert.AreEqual(3, _ioc._services.Count);
            Assert.IsTrue(_ioc._services.Any(s => s.ServiceType.Equals(typeof(INavigationService))));
        }

        [Test]
        public void TestRemovePage()
        {
            Assert.IsTrue(_ioc.RemovePage<Test2Page>());
            Assert.AreEqual(1, _ioc._pages.Count);
            Assert.AreEqual(typeof(TestPage), _ioc._pages[0].PageType);
            Assert.AreEqual(typeof(TestViewModel), _ioc._pages[0].VmType);
        }

        [Test]
        public void TestRemovePageNotRegistered()
        {
            Assert.IsFalse(_ioc.RemovePage<ContentPage>());
            Assert.AreEqual(3, _ioc._pages.Count);
        }

        [Test]
        public void TestRemovePageAndViewModelCombination()
        {
            Assert.IsTrue(_ioc.RemovePage<Test2Page, Test2ViewModel>());
            Assert.AreEqual(2, _ioc._pages.Count);
        }

        [Test]
        public void TestRemovePageAndViewModelCombinationNotRegistered()
        {
            Assert.IsFalse(_ioc.RemovePage<TestPage, Test2ViewModel>());
            Assert.AreEqual(3, _ioc._pages.Count);
        }


    }
}
