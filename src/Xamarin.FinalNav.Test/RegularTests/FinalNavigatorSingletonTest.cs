using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.FinalNav.Test
{
    public class FinalNavigatorSingletonTest
    {

        [Test]
        public void InstancesEqualTest()
        {
            var instance1 = FinalNavigator.Instance;
            var instance2 = FinalNavigator.Instance;

            Assert.AreSame(instance1, instance2);
        }

    }
}
