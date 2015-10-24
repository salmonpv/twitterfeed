using AG_Twitter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG_Twitter.Repositories
{
    public class UserRepository
    {
        private string mFilename;
        public UserRepository(string filename)
        {
            mFilename = filename;
        }

        public static User GetOrCreateUser(string name, Dictionary<string, User> existing)
        {
            User find = null;
            string upperTest = name.ToUpper();
            if (existing.TryGetValue(upperTest, out find) == false)
            {
                find = new User(name);
                existing.Add(upperTest, find);
            }
            return find;
        }
        public static void UpdateFromLine(string line, Dictionary<string, User> existing)
        {
            var splitFollows = line.Split(new string[]{" follows "}, StringSplitOptions.RemoveEmptyEntries);
            if (splitFollows.Length == 0)
                return;
            User current = null;
            if (splitFollows.Length > 0)
            {
                string userKey = splitFollows[0].Trim();
                current = GetOrCreateUser(userKey, existing);
            }
            if (splitFollows.Length > 1)
            {
                var splitUsers = splitFollows[1].Split(',');
                foreach (var userSplitKey in splitUsers)
                {
                    var trimSplitKey = userSplitKey.Trim();
                    var upperTest = trimSplitKey.ToUpper();
                    if (string.IsNullOrEmpty(trimSplitKey) == false && current.Follows.Any(p => p.Name.ToUpper() == upperTest) == false)
                        current.Follows.Add(GetOrCreateUser(userSplitKey.Trim(), existing));
                }
            }
            return;
        }
        public IEnumerable<User> GetAll()
        {
            var users = new Dictionary<string, User>();
            using (var sr = new StreamReader(mFilename))
            {
                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;

                    UpdateFromLine(line, users);
                }
                
            }
            return users.Values;
        }
    }
}
