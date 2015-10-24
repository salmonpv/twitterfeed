using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AG_Twitter.Models;
using AG_Twitter.Repositories;

namespace AG_TwitterTest
{
    [TestClass]
    public class UserReadTests
    {
        [TestMethod]
        public void TestFromLineNoPriorUsers()
        {
            Dictionary<string, User> existing = new Dictionary<string,User>();
            UserRepository.UpdateFromLine("Ward follows Alan", existing);
            Assert.IsTrue(existing.ContainsKey("ALAN"));
            Assert.IsTrue(existing.ContainsKey("WARD"));
            Assert.AreSame(existing["WARD"].Follows[0], existing["ALAN"]);
        }


        [TestMethod]
        public void TestFromLinePriorUsers()
        {
            Dictionary<string, User> existing = new Dictionary<string, User>();
            UserRepository.UpdateFromLine("Ward follows Alan", existing);
            UserRepository.UpdateFromLine("Mike follows Ward", existing);
            Assert.IsTrue(existing.ContainsKey("MIKE"));
            Assert.AreSame(existing["MIKE"].Follows[0], existing["WARD"]);
        }

        [TestMethod]
        public void TestMoreThanOneFollows()
        {
            Dictionary<string, User> existing = new Dictionary<string, User>();
            UserRepository.UpdateFromLine("Ward follows Alan, Mike", existing);
            
            Assert.IsTrue(existing["WARD"].Follows.Contains(existing["ALAN"]));
            Assert.IsTrue(existing["WARD"].Follows.Contains(existing["MIKE"]));
        }

        [TestMethod]
        public void TestWithNoFollows()
        {
            Dictionary<string, User> existing = new Dictionary<string, User>();
            UserRepository.UpdateFromLine("WARD", existing);

            Assert.AreEqual(0, existing["WARD"].Follows.Count);
        }

        [TestMethod]
        public void TestNoDuplicateFollowers()
        {
            Dictionary<string, User> existing = new Dictionary<string, User>();
            UserRepository.UpdateFromLine("Ward follows Alan", existing);
            UserRepository.UpdateFromLine("Alan follows Martin", existing);
            UserRepository.UpdateFromLine("Ward follows Martin, Alan", existing);

            Assert.AreEqual(2, existing["WARD"].Follows.Count);
        }

        [TestMethod]
        public void TestFollowsIsIgnoredInNames()
        {
            Dictionary<string, User> existing = new Dictionary<string, User>();
            UserRepository.UpdateFromLine("Wfollowsard follows Alan", existing);
            UserRepository.UpdateFromLine("Sam follows Marfollowstin", existing);

            Assert.IsTrue(existing.ContainsKey("WFOLLOWSARD"));
            Assert.IsTrue(existing.ContainsKey("ALAN"));
            Assert.IsTrue(existing.ContainsKey("SAM"));
            Assert.IsTrue(existing.ContainsKey("MARFOLLOWSTIN"));
        }

        [TestMethod]
        public void TestCaseSensitivity()
        {
            Dictionary<string, User> existing = new Dictionary<string, User>();
            UserRepository.UpdateFromLine("Ward follows Alan", existing);
            UserRepository.UpdateFromLine("WARD follows Martin", existing);
            UserRepository.UpdateFromLine("Ward follows Martin, ALAN", existing);

            Assert.IsTrue(existing.ContainsKey("WARD"));
            Assert.IsTrue(existing.ContainsKey("ALAN"));
            Assert.IsTrue(existing.ContainsKey("MARTIN"));
            Assert.AreEqual(3, existing.Count);
        }


    }
}
