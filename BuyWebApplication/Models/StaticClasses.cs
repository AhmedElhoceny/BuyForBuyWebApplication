using BuyWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElMarket.Models
{
    public static class StaticClasses
    {
        public static ContextClass HomeContextObject { get; set; }
        public static ContextClass AdminContextObject { get; set; }
        public static ContextClass OrderMasterContextObject { get; set; }
        public static ContextClass UserContextObject { get; set; }
    }
}