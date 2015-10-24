using AG_Twitter.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG_Twitter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Twitter feed");
            string userFileName = "user.txt";
            string tweetFileName = "tweet.txt";

            if (args.Length > 1)
            {
                userFileName = args[0];
                tweetFileName = args[1];
            }
            FeedReader.ReadFeeds(userFileName, tweetFileName);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
