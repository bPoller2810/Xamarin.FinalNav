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
    public class FinalIocTestRegisterService
    {

        private FinalIoc _ioc;

        [SetUp]
        public void InitTest()
        {
            _ioc = new FinalIoc();
        }

        [Test]
        public void TestRegisterServiceDefaultParameters()
        {
            _ioc.RegisterService<ITestService, TestService>();

            Assert.AreEqual(1, _ioc._services.Count);
            Assert.AreEqual(typeof(ITestService), _ioc._services[0].ServiceType);
            Assert.AreEqual(typeof(TestService), _ioc._services[0].ServiceImplementation);
            Assert.AreEqual(EServiceLifetime.Singleton, _ioc._services[0].Lifetime);
        }

        [Test]
        public void TestRegisterServiceLifetimeNewInstance()
        {
            _ioc.RegisterService<ITestService, TestService>(EServiceLifetime.NewInstance);

            Assert.AreEqual(1, _ioc._services.Count);
            Assert.AreEqual(EServiceLifetime.NewInstance, _ioc._services[0].Lifetime);
        }

        [Test]
        public void TestRegisterServiceThrowsOnNoInterface()
        {
            Assert.Throws<NotSupportedException>(() => _ioc.RegisterService<TestService, TestService>());
            Assert.AreEqual(0, _ioc._services.Count);
        }

        [Test]
        public void TestRegisterServiceThrowsOnDoubleRegistration()
        {
            _ioc.RegisterService<ITestService, TestService>();
            Assert.Throws<ArgumentException>(() => _ioc.RegisterService<ITestService, TestService>());
            Assert.AreEqual(1, _ioc._services.Count);
        }

        [Test]
        public void TestRegisterServiceWithInstance()
        {
            var instance = new TestService();

            _ioc.RegisterService<ITestService>(instance);

            Assert.AreEqual(1, _ioc._services.Count);
            Assert.AreEqual(typeof(ITestService), _ioc._services[0].ServiceType);
            Assert.AreEqual(typeof(TestService), _ioc._services[0].ServiceImplementation);
            Assert.AreEqual(instance, _ioc._services[0].Instance);
        }

        [Test]
        public void TestRegisterServiceWithInstanceThrowsOnInstanceNull()
        {
            Assert.Throws<ArgumentNullException>(() => _ioc.RegisterService<TestService>(null));
            Assert.AreEqual(0, _ioc._services.Count);
        }

        [Test]
        public void TestRegisterServiceWithInstanceThrowsOnNoInterface()
        {
            Assert.Throws<NotSupportedException>(() => _ioc.RegisterService<TestService>(new TestService()));
            Assert.AreEqual(0, _ioc._services.Count);
        }

        [Test]
        public void TestRegisterServiceWithInstanceThrowsOnDoubleRegistration()
        {
            _ioc.RegisterService<ITestService>(new TestService());
            Assert.Throws<ArgumentException>(() => _ioc.RegisterService<ITestService>(new TestService()));
            Assert.AreEqual(1, _ioc._services.Count);
        }

    }
}
