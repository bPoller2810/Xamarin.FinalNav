using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.FinalNav.Test.XFMock
{
    public static class MockXF
    {

        public static void Init()
        {
            Device.Info = new MockDeviceInfo();
            Device.PlatformServices = new MockXFPlatform();

            DependencyService.Register<MockResourcesProvider>();
            DependencyService.Register<MockDeserializer>();
        }

    }
}
