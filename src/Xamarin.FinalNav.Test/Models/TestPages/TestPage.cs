using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.FinalNav.Test.Models.TestPages
{
    public class TestPage : ContentPage
    {

        public int MessageId { get; }

        public TestPage()
        {
        }
        public TestPage(int messageId)
        {
            MessageId = messageId;
        }

    }
}
