using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AG_Twitter.Repositories;
using AG_Twitter.Models;

namespace AG_TwitterTest
{
    [TestClass]
    public class TweetReadTests
    {
        [TestMethod]
        public void TestSimpleTweetItem()
        {
            Tweet tweet;
            TweetRepository.GetFromLine("Alan> If you have a procedure with 10 parameters, you probably missed some.", 0, out tweet);
            Assert.AreEqual("Alan", tweet.Username);
            Assert.AreEqual("If you have a procedure with 10 parameters, you probably missed some.", tweet.Comment);
        }

        [TestMethod]
        public void TestCommentWithGreaterAs()
        {
            Tweet tweet;
            TweetRepository.GetFromLine("Alan> If you have a procedure with > 10 parameters, you probably missed some.", 0, out tweet);
            Assert.AreEqual("Alan", tweet.Username);
            Assert.AreEqual("If you have a procedure with > 10 parameters, you probably missed some.", tweet.Comment);
        }

        [TestMethod]
        public void TestEmptyTweetsShouldBeIgnored()
        {
            Tweet tweet;
            var added = TweetRepository.GetFromLine("Alan> ", 0, out tweet);
            Assert.IsFalse(added);

            added = TweetRepository.GetFromLine("Alan", 0, out tweet);
            Assert.IsFalse(added);

            added = TweetRepository.GetFromLine("", 0, out tweet);
            Assert.IsFalse(added);

            added = TweetRepository.GetFromLine(null, 0, out tweet);
            Assert.IsFalse(added);
        }
    }
}
