using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElMarket.ViewModels
{
    public class AdminViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string ConfirmPassWord { get; set; }
    }
}