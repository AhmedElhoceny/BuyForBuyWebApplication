using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElMarket.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; } 
        public string UserName { get; set; }
        public string SecretKey { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public int Rank { get; set; }
        public int OperationsNumber { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
    }
}