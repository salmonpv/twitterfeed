using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG_Twitter.Utilities
{
    public class ConvertToASCII
    {
        public static void Convert(string inputFileName, string outputFileName)
        {
            var input = File.ReadAllText(inputFileName);

            using (var sw = new StreamWriter(outputFileName, false, ASCIIEncoding.ASCII))
            {
                sw.Write(input);
            }
        }
    }
}
