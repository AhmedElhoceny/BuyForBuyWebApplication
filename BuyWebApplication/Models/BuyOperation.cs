using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElMarket.Models
{
    public class BuyOperation
    {
        [Key]
        public int OpeartionId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Bill { get; set; }
        public DateTime ProductTime { get; set; }
        public string Country { get; set; }
        public int OrderMasterID { get; set; }
        public bool DoneOrNot { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string type { get; set; }
    }
}