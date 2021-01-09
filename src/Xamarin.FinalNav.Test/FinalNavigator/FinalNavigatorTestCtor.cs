using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.FinalNav.Test.XFMock;
using Xamarin.Forms;

namespace Xamarin.FinalNav.Test
{
    [TestFixture]
    public class FinalNavigatorTestCtor
    {

        private FinalIoc _ioc;
        private Application _app;

        [SetUp]
        public void InitTest()
        {
            MockXF.Init();
            _ioc = new FinalIoc();
            _app = new Application();
        }

        [Test]
        public void TestCtor()
        {
            new FinalNavigator(_app, _ioc);

            Assert.AreEqual(1, _ioc._services.Count);
            Assert.AreEqual(typeof(INavigationService),_ioc._services[0].ServiceType);
            Assert.AreEqual(typeof(FinalNavigator), _ioc._services[0].ServiceImplementation);
        }

        [Test]
        public void TestCtorThrowsOnAppNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new FinalNavigator(null, _ioc));
            Assert.AreEqual("app", exception.ParamName);
        }

        [Test]
        public void TestCtorThrowsOnIocNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new FinalNavigator(_app, null));
            Assert.AreEqual("iocContainer", exception.ParamName);
        }

    }
}
