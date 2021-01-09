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
    public class FinalNavigatorTestPopStacksAsync
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
        public async Task TestPopAsync()
        {
            _navigator.Build<TestPage>();
            await _navigator.PushAsync<Test2Page, Test2ViewModel>();
            await _navigator.PushAsync<Test3Page>();

            await _navigator.PopAsync();

            Assert.AreEqual(2, _navigator._navigation.NavigationStack.Count);
            Assert.IsInstanceOf<TestPage>(_navigator._navigation.NavigationStack[0]);
            Assert.IsInstanceOf<Test2Page>(_navigator._navigation.NavigationStack[1]);
        }

        [Test]
        public void TestPopAsyncNotInitialized()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _navigator.PopAsync());
        }

        [Test]
        public async Task TestPopToRootAsync()
        {
            _navigator.Build<TestPage>();
            await _navigator.PushAsync<Test2Page, Test2ViewModel>();
            await _navigator.PushAsync<Test3Page>();

            await _navigator.PopToRootAsync();

            Assert.AreEqual(1, _navigator._navigation.NavigationStack.Count);
            Assert.IsInstanceOf<TestPage>(_navigator._navigation.NavigationStack[0]);
        }

        [Test]
        public void TestPopToRootAsyncNotInitialized()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _navigator.PopToRootAsync());
        }

        [Test]
        public async Task TestPopModalAsync()
        {
            _navigator.Build<TestPage>();
            await _navigator.PushModalAsync<TestPage>();
            await _navigator.PushModalAsync<Test2Page>();
            await _navigator.PushModalAsync<Test3Page>();

            await _navigator.PopModalAsync();

            Assert.AreEqual(2, _navigator._navigation.ModalStack.Count);
            Assert.IsInstanceOf<TestPage>(_navigator._navigation.ModalStack[0]);
            Assert.IsInstanceOf<Test2Page>(_navigator._navigation.ModalStack[1]);
        }

        //pop modal single page

    }
}
