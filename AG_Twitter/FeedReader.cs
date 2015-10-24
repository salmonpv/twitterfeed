using AG_Twitter.Models;
using AG_Twitter.Repositories;
using AG_Twitter.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG_Twitter
{
    public class FeedReader
    {
        public static IEnumerable<Tweet> GetTweetsForUser(User user, ILookup<string, Tweet> tweets)
        {
            var result = new List<Tweet>();
            result.AddRange(tweets[user.Name.ToUpper()]);
            foreach (var follow in user.Follows)
            {
                result.AddRange(tweets[follow.Name.ToUpper()]);
            }
            return result.OrderBy(p => p.Sequence);
        }
        public static void ReadFeeds(string userFileName, string tweetFileName)
        {
            try
            {
                var users = new UserRepository(userFileName).GetAll().OrderBy(p => p.Name);
                var tweets = new TweetRepository(tweetFileName).GetAll().ToLookup(p => p.Username.ToUpper());
                foreach (var user in users)
                {
                    Console.WriteLine(user.Name);
                    foreach (var tweet in GetTweetsForUser(user, tweets))
                    {
                        Console.Write("\t@");
                        Console.Write(tweet.Username);
                        Console.Write(": ");
                        Console.WriteLine(tweet.Comment);
                    }
                }
            }
            catch (Exception ex)
            {
                SimpleFileLogger.LogEvent("Read tweets exception", DateTime.Now, ex.Message, true);

            }
        }
    }
}
