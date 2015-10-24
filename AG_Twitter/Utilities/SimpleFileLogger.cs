using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG_Twitter.Utilities
{
    public class SimpleFileLogger
    {
        public static void LogEvent(string name, DateTime timeStamp, string message, bool showInConsole = false)
        {
            try
            {
                using (var sw = new StreamWriter(string.Concat("LOG_", DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, ".txt"), true, ASCIIEncoding.ASCII))
                {
                    sw.Write(name);
                    sw.Write("/t");
                    sw.Write(timeStamp.ToString());
                    sw.Write("/t");
                    sw.WriteLine(message);
                }
                if (showInConsole)
                {
                    Console.Write(name);
                    Console.Write("/t");
                    Console.Write(timeStamp.ToString());
                    Console.Write("/t");
                    Console.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not write to log {0}", ex.Message);
            }
        }
    }
}
