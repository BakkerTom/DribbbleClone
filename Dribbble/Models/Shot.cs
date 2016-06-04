using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dribbble.Models.Repositories;

namespace Dribbble.Models
{
    public class Shot
    {
        private AccountRepository accountRepo = new AccountRepository();

        public int ID { get; set; }
        public int AccountID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int ReboundID { get; set; }
        public DateTime CreationDate { get; set; }
        public HttpPostedFileBase file { get; set; }

        public Account GetAccount()
        {
            return accountRepo.getByID(AccountID);
        }
    }
}