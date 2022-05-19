using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElMarket.Models
{
    public class Categories
    {
        [Key]
        public int Categories_Id { get; set; }
        public string Title { get; set; }
    }
}