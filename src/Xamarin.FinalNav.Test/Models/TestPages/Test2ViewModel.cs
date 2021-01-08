using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.FinalNav.Test.Models.TestServices;

namespace Xamarin.FinalNav.Test.Models.TestPages
{
    public class Test2ViewModel : INotifyPropertyChanged
    {

        public int MessageId { get; }
        public ITestService TestService { get; }

        public Test2ViewModel()
        {
        }
        public Test2ViewModel(int messageId)
        {
            MessageId = messageId;
        }
        public Test2ViewModel(ITestService testService)
        {
            TestService = testService;
        }
        public Test2ViewModel(ITestService testService, int messageId)
        {
            TestService = testService;
            MessageId = messageId;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
