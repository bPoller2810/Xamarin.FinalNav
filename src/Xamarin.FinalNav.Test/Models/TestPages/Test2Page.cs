using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.FinalNav.Test.Models.TestServices;
using Xamarin.Forms;

namespace Xamarin.FinalNav.Test.Models.TestPages
{
    public class Test2Page : ContentPage
    {

        public ITestService TestService { get; }
        public int MessageId { get; }

        public Test2Page()
        {
        }
        public Test2Page(ITestService testService)
        {
            TestService = testService;
        }
        public Test2Page(int messageId)
        {
            MessageId = messageId;
        }
        public Test2Page(ITestService testService, int messageId)
        {
            TestService = testService;
            MessageId = messageId;
        }

    }
}
