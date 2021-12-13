using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using System.Threading;

namespace CSHttpClientSample
{
    public class Program
    {
        
        public static void Main()
        {
            Console.WriteLine("Starting timer with callback every 1 second.");
            Timer timer = new Timer(callback, "Some state", TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            //Thread.Sleep(4500); // Wait a bit over 4 seconds.

            //Console.WriteLine("Changing timer to callback every 2 seconds.");
            //timer.Change(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
            //Thread.Sleep(9000);
            //timer.Change(-1, -1); // Stop the timer from running.

            Console.WriteLine("Done. Press ENTER");
            Console.ReadLine();
        }
        private static void callback(object state)
        {
            Console.WriteLine("Called back with state = " + state);

        }
    }
}