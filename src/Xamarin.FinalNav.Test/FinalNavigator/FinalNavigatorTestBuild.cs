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

            _navigator = new FinalNavigator(_app, _ioc);
        }

        [Test]
        public void TestBuild()
        {
            _navigator.Build<TestPage>();
            Assert.IsInstanceOf<NavigationPage>(_navigator._app.MainPage);
            Assert.IsInstanceOf<TestPage>(((NavigationPage)_navigator._app.MainPage).RootPage);
            Assert.NotNull(_navigator._navigation);
            Assert.IsTrue(_navigator.Initialized);
        }

        [Test]
        public void TestBuildPageNotFound()
        {
            Assert.Throws<ArgumentException>(() => _navigator.Build<Test2Page>());
            Assert.Null(_navigator._app.MainPage);
            Assert.Null(_navigator._navigation);
            Assert.False(_navigator.Initialized);
        }

    }
}
