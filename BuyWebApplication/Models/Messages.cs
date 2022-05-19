using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElMarket.Models
{
    public class Messages
    {
        [Key]
        public int id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
        public DateTime MessageTime { get; set; }
    }
}