using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FinalNav.Sample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalNav.Sample.Droid.Service
{
    public class AndroidPlatformService : IPlatformDependentService
    {
        public string GetInfo()
        {
            return "Service implemented on Android only";
        }
    }
}