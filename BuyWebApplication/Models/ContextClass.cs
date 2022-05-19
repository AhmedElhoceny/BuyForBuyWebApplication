using ElMarket.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyWebApplication.Models
{
    public class ContextClass : DbContext
    {


        public ContextClass(DbContextOptions<ContextClass> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<BuyOperation> BuyOperation { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<OrdersMasters> OrdersMasters { get; set; }
        public DbSet<Categories> Categories { get; set; }
    }
}
