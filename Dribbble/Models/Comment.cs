using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dribbble.Models.Repositories;

namespace Dribbble.Models
{
    public class Comment
    {
        private AccountRepository accountRepo = new AccountRepository();

        public int ID { get; set; }
        public int ShotID { get; set; }
        public int AccountID { get; set; }
        public string Message { get; set; }

        public Account Account
        {
            get { return accountRepo.getByID(AccountID); }
        }

    }
}