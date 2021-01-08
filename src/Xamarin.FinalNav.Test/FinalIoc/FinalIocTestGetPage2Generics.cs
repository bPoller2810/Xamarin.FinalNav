using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.FinalNav.Test.Models.TestPages;
using Xamarin.FinalNav.Test.Models.TestServices;

namespace Xamarin.FinalNav.Test
{
    [TestFixture]
    public class FinalIocTestGetPage2Generics
    {

        private FinalIoc _ioc;

        [SetUp]
        public void InitTest()
        {
            _ioc = new FinalIoc();
        }

        [Test]
        public void TestGetPage()
        {
            _ioc.RegisterPage<TestPage, TestViewModel>();
            _ioc.RegisterPage<TestPage, Test2ViewModel>();

            var page = _ioc.GetPage<TestPage, Test2ViewModel>();

            Assert.IsInstanceOf<TestPage>(page);
            Assert.IsInstanceOf<Test2ViewModel>(page.BindingContext);
        }

        [Test]
        public void TestGetPageNotFound()
        {
            _ioc.RegisterPage<TestPage, TestViewModel>();

            Assert.Throws<ArgumentException>(() => _ioc.GetPage<TestPage, Test2ViewModel>());
        }

        [Test]
        public void TestGetPageWithPageService()
        {
            _ioc.RegisterService<ITestService, TestService>();

            _ioc.RegisterPage<TestPage, TestViewModel>();
            _ioc.RegisterPage<Test2Page, Test2ViewModel>();
            _ioc.RegisterPage<Test2Page, TestViewModel>();

            var page = _ioc.GetPage<Test2Page, TestViewModel>();

            Assert.IsInstanceOf<Test2Page>(page);
            Assert.IsInstanceOf<TestViewModel>(page.BindingContext);

            Assert.NotNull(page.TestService);
        }

        [Test]
        public void TestGetPageWithPageUserParameter()
        {
            _ioc.RegisterPage<TestPage, Test2ViewModel>();
            _ioc.RegisterPage<TestPage, TestViewModel>();
            _ioc.RegisterPage<Test2Page, Test2ViewModel>();

            var id = 1345;
            var page = _ioc.GetPage<TestPage, TestViewModel>(new PageParameter(id));

            Assert.IsInstanceOf<TestPage>(page);
            Assert.IsInstanceOf<TestViewModel>(page.BindingContext);

            Assert.AreEqual(id, page.MessageId);
            Assert.AreEqual(0, ((TestViewModel)page.BindingContext).MessageId);
        }

        [Test]
        public void TestGetPageWithPageUserParameterAndService()
        {
            _ioc.RegisterService<ITestService, TestService>();

            _ioc.RegisterPage<TestPage, TestViewModel>();
            _ioc.RegisterPage<Test2Page, Test2ViewModel>();
            _ioc.RegisterPage<Test2Page, TestViewModel>();

            var id = 1134;
            var page = _ioc.GetPage<Test2Page, TestViewModel>(new PageParameter(id));

            Assert.IsInstanceOf<Test2Page>(page);
            Assert.IsInstanceOf<TestViewModel>(page.BindingContext);

            Assert.AreEqual(id, page.MessageId);
            Assert.NotNull(page.TestService);

            Assert.AreEqual(0, ((TestViewModel)page.BindingContext).MessageId);
        }

        [Test]
        public void TestGetPageWithVMSerivce()
        {
            _ioc.RegisterService<ITestService, TestService>();

            _ioc.RegisterPage<TestPage, TestViewModel>();
            _ioc.RegisterPage<TestPage, Test2ViewModel>();

            var page = _ioc.GetPage<TestPage, Test2ViewModel>();

            Assert.IsInstanceOf<TestPage>(page);
            Assert.IsInstanceOf<Test2ViewModel>(page.BindingContext);

            Assert.NotNull(((Test2ViewModel)page.BindingContext).TestService);
        }

        [Test]
        public void TestGetPageWithVMParameter()
        {
            _ioc.RegisterService<ITestService, TestService>();

            _ioc.RegisterPage<TestPage, Test2ViewModel>();
            _ioc.RegisterPage<TestPage, TestViewModel>();

            var id = 1344;

            var page = _ioc.GetPage<TestPage, TestViewModel>(new ViewModelParameter(id));

            Assert.IsInstanceOf<TestPage>(page);
            Assert.IsInstanceOf<TestViewModel>(page.BindingContext);

            Assert.AreEqual(id, ((TestViewModel)page.BindingContext).MessageId);
        }

        [Test]
        public void TestGetPageWithVMServiceAndParameter()
        {
            _ioc.RegisterService<ITestService, TestService>();

            _ioc.RegisterPage<Test2Page, TestViewModel>();
            _ioc.RegisterPage<TestPage, TestViewModel>();
            _ioc.RegisterPage<TestPage, Test2ViewModel>();

            var id = 1335;
            var page = _ioc.GetPage<TestPage, Test2ViewModel>(new ViewModelParameter(id));

            Assert.IsInstanceOf<TestPage>(page);
            Assert.IsInstanceOf<Test2ViewModel>(page.BindingContext);

            Assert.AreEqual(id, ((Test2ViewModel)page.BindingContext).MessageId);
            Assert.NotNull(((Test2ViewModel)page.BindingContext).TestService);

            Assert.AreEqual(0, page.MessageId);
        }

    }
}
