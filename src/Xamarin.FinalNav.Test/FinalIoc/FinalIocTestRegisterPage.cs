using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.FinalNav.Test.Models.TestPages;

namespace Xamarin.FinalNav.Test
{

    [TestFixture]
    public class FinalIocTestRegisterPage
    {

        private FinalIoc _ioc;

        [SetUp]
        public void InitTest()
        {
            _ioc = new FinalIoc();
        }

        [Test]
        public void TestRegisterPageDefaultParameters()
        {
            _ioc.RegisterPage<TestPage, TestViewModel>();

            Assert.AreEqual(1, _ioc._pages.Count);
            Assert.AreEqual(typeof(TestPage), _ioc._pages[0].PageType);
            Assert.AreEqual(typeof(TestViewModel), _ioc._pages[0].VmType);
        }

        [Test]
        public void TestRegisterPageSamePageDifferentViewModel()
        {
            _ioc.RegisterPage<TestPage, TestViewModel>();
            _ioc.RegisterPage<TestPage, Test2ViewModel>();

            Assert.AreEqual(2, _ioc._pages.Count);
            Assert.AreEqual(typeof(TestPage), _ioc._pages[0].PageType);
            Assert.AreEqual(typeof(TestViewModel), _ioc._pages[0].VmType);
            Assert.AreEqual(typeof(TestPage), _ioc._pages[1].PageType);
            Assert.AreEqual(typeof(Test2ViewModel), _ioc._pages[1].VmType);
        }

        [Test]
        public void TestRegisterPageDifferentPageSameViewModel()
        {
            _ioc.RegisterPage<TestPage, TestViewModel>();
            _ioc.RegisterPage<Test2Page, TestViewModel>();

            Assert.AreEqual(2, _ioc._pages.Count);
            Assert.AreEqual(typeof(TestPage), _ioc._pages[0].PageType);
            Assert.AreEqual(typeof(TestViewModel), _ioc._pages[0].VmType);
            Assert.AreEqual(typeof(Test2Page), _ioc._pages[1].PageType);
            Assert.AreEqual(typeof(TestViewModel), _ioc._pages[1].VmType);
        }

        [Test]
        public void TestRegisterPageThrowsOnDoubleRegistration()
        {
            _ioc.RegisterPage<TestPage, TestViewModel>();

            Assert.Throws<ArgumentException>(() => _ioc.RegisterPage<TestPage, TestViewModel>());
            Assert.AreEqual(1, _ioc._pages.Count);
        }


    }
}
