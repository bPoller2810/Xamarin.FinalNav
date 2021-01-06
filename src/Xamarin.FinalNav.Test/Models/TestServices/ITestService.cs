using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.FinalNav.Test.Models.TestServices
{
    public interface ITestService
    {
        void OutputTime();
    }

    public class TestService : ITestService
    {
        public void OutputTime()
        {
            Console.WriteLine(DateTime.UtcNow);
        }
    }

}
