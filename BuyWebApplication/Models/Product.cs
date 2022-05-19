using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElMarket.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; }
        public string Image { get; set; }
        public string Discription { get; set; }
        public int Category_ID { get; set; }
    }
}