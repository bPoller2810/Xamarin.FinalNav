using System;

namespace Xamarin.FinalNav.Test.Models.TestServices
{
    public class TestService : ITestService
    {
        public void OutputTime()
        {
            Console.WriteLine(DateTime.UtcNow);
        }
    }

}
