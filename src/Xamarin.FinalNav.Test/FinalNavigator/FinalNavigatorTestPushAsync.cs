using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.FinalNav.Test.Models.TestPages;
using Xamarin.FinalNav.Test.XFMock;
using Xamarin.Forms;

namespace Xamarin.FinalNav.Test
{
    [TestFixture]
    public class FinalNavigatorTestPushAsync
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
            _ioc.RegisterPage<Test2Page, Test2ViewModel>();
            _ioc.RegisterPage<Test2Page, TestViewModel>();
            _ioc.RegisterPage<Test3Page, TestViewModel>();
            _ioc.RegisterPage<Test3Page, Test2ViewModel>();

            _navigator = new FinalNavigator(_app, _ioc);
        }

        [Test]
        public async Task TestPushAsync1Generic()
        {
            _navigator.Build<TestPage>();
            await _navigator.PushAsync<Test2Page>();

            Assert.IsInstanceOf<TestPage>(_navigator._navigation.NavigationStack[0]);
            Assert.IsInstanceOf<TestViewModel>(_navigator._navigation.NavigationStack[0].BindingContext);
            Assert.IsInstanceOf<Test2Page>(_navigator._navigation.NavigationStack[1]);
            Assert.IsInstanceOf<Test2ViewModel>(_navigator._navigation.NavigationStack[1].BindingContext);
        }

        [Test]
        public void TestPushAsync1GenericThrowsOnNotInitialized()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _navigator.PushAsync<TestPage>());
        }

        [Test]
        public async Task TestPushAsync1GenericWithPageParameter()
        {
            _navigator.Build<TestPage>();
            var id = 1334;
            await _navigator.PushAsync<TestPage>(new PageParameter(id));

            Assert.IsInstanceOf<TestPage>(_navigator._navigation.NavigationStack[1]);
            Assert.IsInstanceOf<TestViewModel>(_navigator._navigation.NavigationStack[1].BindingContext);
            Assert.AreEqual(id, ((TestPage)_navigator._navigation.NavigationStack[1]).MessageId);
            Assert.AreEqual(0, ((TestViewModel)_navigator._navigation.NavigationStack[1].BindingContext).MessageId);
        }

        [Test]
        public async Task TestPushAsync1GenericWithViewModelParameter()
        {
            _navigator.Build<TestPage>();
            var id = 1734;
            await _navigator.PushAsync<TestPage>(new ViewModelParameter(id));

            Assert.IsInstanceOf<TestPage>(_navigator._navigation.NavigationStack[1]);
            Assert.IsInstanceOf<TestViewModel>(_navigator._navigation.NavigationStack[1].BindingContext);
            Assert.AreEqual(0, ((TestPage)_navigator._navigation.NavigationStack[1]).MessageId);
            Assert.AreEqual(id, ((TestViewModel)_navigator._navigation.NavigationStack[1].BindingContext).MessageId);
        }

        [Test]
        public async Task TestPushAsync2Generic()
        {
            _navigator.Build<TestPage>();
            await _navigator.PushAsync<Test3Page, Test2ViewModel>();

            Assert.IsInstanceOf<TestPage>(_navigator._navigation.NavigationStack[0]);
            Assert.IsInstanceOf<TestViewModel>(_navigator._navigation.NavigationStack[0].BindingContext);
            Assert.IsInstanceOf<Test3Page>(_navigator._navigation.NavigationStack[1]);
            Assert.IsInstanceOf<Test2ViewModel>(_navigator._navigation.NavigationStack[1].BindingContext);
        }

        [Test]
        public void TestPushAsync2GenericThrowsOnNotInitialized()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _navigator.PushAsync<TestPage,TestViewModel>());
        }

        [Test]
        public async Task TestPushAsync2GenericWithPageParameter()
        {
            _navigator.Build<TestPage>();
            var id = 1247;
            await _navigator.PushAsync<Test2Page,TestViewModel>(new PageParameter(id));

            Assert.IsInstanceOf<Test2Page>(_navigator._navigation.NavigationStack[1]);
            Assert.IsInstanceOf<TestViewModel>(_navigator._navigation.NavigationStack[1].BindingContext);
            Assert.AreEqual(id, ((Test2Page)_navigator._navigation.NavigationStack[1]).MessageId);
            Assert.AreEqual(0, ((TestViewModel)_navigator._navigation.NavigationStack[1].BindingContext).MessageId);
        }

        [Test]
        public async Task TestPushAsync2GenericWithViewModelParameter()
        {
            _navigator.Build<TestPage>();
            var id = 1734;
            await _navigator.PushAsync<Test2Page,TestViewModel>(new ViewModelParameter(id));

            Assert.IsInstanceOf<Test2Page>(_navigator._navigation.NavigationStack[1]);
            Assert.IsInstanceOf<TestViewModel>(_navigator._navigation.NavigationStack[1].BindingContext);
            Assert.AreEqual(0, ((Test2Page)_navigator._navigation.NavigationStack[1]).MessageId);
            Assert.AreEqual(id, ((TestViewModel)_navigator._navigation.NavigationStack[1].BindingContext).MessageId);
        }

    }
}
