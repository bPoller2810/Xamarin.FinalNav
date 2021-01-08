using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.FinalNav.Test.Models.TestServices;
using Xamarin.FinalNav.Test.XFMock;
using Xamarin.Forms;

namespace Xamarin.FinalNav.Test
{
    [TestFixture]
    public class FinalIocTestGetService
    {

        private FinalIoc _ioc;

        [SetUp]
        public void InitTest()
        {
            _ioc = new FinalIoc();

            _ioc.RegisterService<ITestService, TestService>(EServiceLifetime.Singleton);
            _ioc.RegisterService<ITest2Service, Test2Service>(EServiceLifetime.NewInstance);
        }

        [Test]
        public void TestGetService()
        {
            var service = _ioc.GetService<ITestService>();
            
            Assert.IsInstanceOf<TestService>(service);
            Assert.IsInstanceOf<ITestService>(service);
        }

        [Test]
        public void TestGetServiceNotFound()
        {
            Assert.Throws<ArgumentException>(() => _ioc.GetService<ITest3Service>());
        }

        [Test]
        public void TestGetServiceSingleton()
        {
            var service1 = _ioc.GetService<ITestService>();
            var service2 = _ioc.GetService<ITestService>();

            Assert.AreEqual(service1, service2);
        }

        [Test]
        public void TestGetServiceNewInstance()
        {
            var service1 = _ioc.GetService<ITest2Service>();
            var service2 = _ioc.GetService<ITest2Service>();

            Assert.AreNotEqual(service1, service2);
        }

    }
}
