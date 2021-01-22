using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.FinalNav.Test.Models.TestPages;
using Xamarin.FinalNav.Test.XFMock;
using Xamarin.Forms;

namespace Xamarin.FinalNav.Test
{
    [TestFixture]
    public class FinalNavigatorTestBuild
    {
        private const string _testString = "MyTestString11§";

        private FinalIoc _ioc;
        private Application _app;
        private FinalNavigator _navigator;

        [SetUp]
        public void InitTest()
        {
            MockXF.Init();

            _ioc = new FinalIoc();
            _app = new Application();

            _ioc.RegisterPage<TestPage, TestViewModel>();
            _ioc.RegisterPage<TestPage, Test2ViewModel>();

            _navigator = new FinalNavigator(_app, _ioc);
        }

        [Test]
        public void TestBuild1Generic()
        {
            _navigator.Build<TestPage>();
            Assert.IsInstanceOf<NavigationPage>(_navigator._app.MainPage);
            Assert.IsInstanceOf<TestPage>(((NavigationPage)_navigator._app.MainPage).RootPage);
            Assert.IsInstanceOf<TestViewModel>(((NavigationPage)_navigator._app.MainPage).RootPage.BindingContext);
            Assert.NotNull(_navigator._navigation);
            Assert.IsTrue(_navigator.Initialized);
        }

        [Test]
        public void TestBuild1GenericPageNotFound()
        {
            Assert.Throws<ArgumentException>(() => _navigator.Build<Test2Page>());
            Assert.Null(_navigator._app.MainPage);
            Assert.Null(_navigator._navigation);
            Assert.False(_navigator.Initialized);
        }

        [Test]
        public void TestBuild1GenericModifyNavPage()
        {
            _navigator.Build<TestPage>(n => n.Title = _testString);
            Assert.AreEqual(_testString, _navigator._navigationPage.Title);
        }

        [Test]
        public void TestBuild2Generic()
        {
            _navigator.Build<TestPage, Test2ViewModel>();
            Assert.IsInstanceOf<NavigationPage>(_navigator._app.MainPage);
            Assert.IsInstanceOf<TestPage>(((NavigationPage)_navigator._app.MainPage).RootPage);
            Assert.IsInstanceOf<Test2ViewModel>(((NavigationPage)_navigator._app.MainPage).RootPage.BindingContext);
            Assert.NotNull(_navigator._navigation);
            Assert.IsTrue(_navigator.Initialized);
        }

        [Test]
        public void TestBuild2GenericPageNotFound()
        {
            Assert.Throws<ArgumentException>(() => _navigator.Build<Test2Page,Test2ViewModel>());
            Assert.Null(_navigator._app.MainPage);
            Assert.Null(_navigator._navigation);
            Assert.False(_navigator.Initialized);
        }

        [Test]
        public void TestBuild2GenericViewModelNotFound()
        {
            Assert.Throws<ArgumentException>(() => _navigator.Build<TestPage, Test3ViewModel>());
            Assert.Null(_navigator._app.MainPage);
            Assert.Null(_navigator._navigation);
            Assert.False(_navigator.Initialized);
        }

        [Test]
        public void Build2GenericModifyNavPage()
        {
            _navigator.Build<TestPage,Test2ViewModel>(n => n.Title = _testString);
            Assert.AreEqual(_testString, _navigator._navigationPage.Title);
        }


    }
}
