using AG_Twitter.Models;
using AG_Twitter.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG_Twitter.Repositories
{
    public class TweetRepository
    {
        private string mFilename;
        public TweetRepository(string filename)
        {
            mFilename = filename;
        }
        public static bool GetFromLine(string line, int sequence, out Tweet tweet)
        {
            tweet = null;
            if (line == null)
                return false;

            StringBuilder user = new StringBuilder();
            StringBuilder comment = new StringBuilder();

            bool foundSeperator = false;
            for (int i = 0; i < line.Length; i++)
            {
                if (!foundSeperator)
                {
                    if (line[i] == '>')
                    {
                        foundSeperator = true;
                        continue;
                    }
                    user.Append(line[i]);
                }
                else
                {
                    comment.Append(line[i]);
                }
            }
            if (!foundSeperator)
            {
                SimpleFileLogger.LogEvent("Tweet format error", DateTime.Now, string.Concat("Did not find feed seperator on line ", sequence));
                return false;
            }

            var commentResult = comment.ToString().Trim();
            if (string.IsNullOrEmpty(commentResult))
            {
                SimpleFileLogger.LogEvent("Tweet format error", DateTime.Now, string.Concat("Feed did not contain a message on line ", sequence));
                return false;
            }
            tweet = new Tweet() { Username = user.ToString().Trim(), Comment = commentResult, Sequence = sequence };
            return true;
        }

        public IEnumerable<Tweet> GetAll()
        {
            var tweets = new List<Tweet>();
            Tweet tweet;
            
                using (var sr = new StreamReader(mFilename))
                {
                    string line;
                    int sequence = 0;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (string.IsNullOrEmpty(line))
                            continue;
                        if (GetFromLine(line, sequence++, out tweet))
                        {
                            tweets.Add(tweet);
                        }
                    }

                }
            
            return tweets;
        }
    }
}
