using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG_Twitter.Models
{
    public class User
    {
        public string Name { get; set; }
        public List<User> Follows { get; set; }

        public User(string name)
        {
            Name = name;
            Follows = new List<User>();
        }
    }
}
