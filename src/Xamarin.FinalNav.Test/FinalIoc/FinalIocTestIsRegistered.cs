using NUnit.Framework;
using Xamarin.FinalNav.Test.Models.TestPages;
using Xamarin.FinalNav.Test.Models.TestServices;

namespace Xamarin.FinalNav.Test
{
    [TestFixture]
    public class FinalIocTestIsRegistered
    {

        private FinalIoc _ioc;

        [SetUp]
        public void InitTest()
        {
            _ioc = new FinalIoc();
            _ioc.RegisterPage<TestPage, TestViewModel>();
            _ioc.RegisterPage<Test2Page, TestViewModel>();
            _ioc.RegisterService<ITestService, TestService>();
            _ioc.RegisterService<ITest2Service, Test2Service>();
        }

        [Test]
        public void TestIsRegisteredPage()
        {
            Assert.IsTrue(_ioc.IsRegistered<TestPage>());
        }

        [Test]
        public void TestIsRegisteredPageNotRegistered()
        {
            Assert.IsFalse(_ioc.IsRegistered<Test3Page>());
        }

        [Test]
        public void TestIsRegisteredService()
        {
            Assert.IsTrue(_ioc.IsRegistered<ITestService>());
        }

        [Test]
        public void TestIsRegisteredServiceNotRegistered()
        {
            Assert.IsFalse(_ioc.IsRegistered<ITest3Service>());
        }

    }
}
