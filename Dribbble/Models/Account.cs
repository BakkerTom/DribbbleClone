using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dribbble.Models
{
    public class Account
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public string Bio { get; set; }

    }
}