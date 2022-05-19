using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElMarket.Models
{
    public class OrdersMasters 
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public int Grad { get; set; }
        public bool Bazy { get; set; }
    }
}