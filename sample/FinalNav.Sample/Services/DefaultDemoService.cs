using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FinalNav.Sample.Services
{
    public class DefaultDemoService : IDemoService
    {
        public void LogStuff(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
