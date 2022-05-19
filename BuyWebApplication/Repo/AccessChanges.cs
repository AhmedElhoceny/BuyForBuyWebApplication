using BuyWebApplication.Models;
using ElMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElMarket.Repo
{
    public class AccessChanges
    {
        public void ChangeDB(ContextClass DB)
        {
            DB.SaveChanges();
        }
    }
}