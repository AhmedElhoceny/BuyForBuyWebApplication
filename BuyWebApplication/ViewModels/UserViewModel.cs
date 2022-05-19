using ElMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElMarket.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PassWord { get; set; }
        public string Phone { get; set; }
        public string SecretKey { get; set; }
    }

}