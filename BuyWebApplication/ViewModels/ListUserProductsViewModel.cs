using ElMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElMarket.ViewModels
{
    public class ListUserProductsViewModel
    {
        public List<Product> UserProducts { get; set; }
        public List<Categories> AllCategories { get; set; }
    }
}