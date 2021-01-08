using System.ComponentModel;

namespace Xamarin.FinalNav.Test.Models.TestPages
{
    public class TestViewModel : INotifyPropertyChanged
    {
        public int MessageId { get; }

        public TestViewModel()
        {
        }
        public TestViewModel(int messageId)
        {
            MessageId = messageId;
        }
      
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
