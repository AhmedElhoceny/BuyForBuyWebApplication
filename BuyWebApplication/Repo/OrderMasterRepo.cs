using BuyWebApplication.Models;
using ElMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElMarket.Repo
{
    public class OrderMasterRepo : Generalinteface<OrdersMasters>
    {
        private readonly ContextClass dB;

        public OrderMasterRepo(ContextClass DB)
        {
            dB = DB;
        }
        public void AddNewItem(OrdersMasters NewItem)
        {
            dB.OrdersMasters.Add(NewItem);
            dB.SaveChanges();
        }

        public void DeleteItem(int Id)
        {
            dB.OrdersMasters.Remove(dB.OrdersMasters.Where(x => x.Id == Id).FirstOrDefault());
            dB.SaveChanges();
        }

        public List<OrdersMasters> GetAllData()
        {
            return dB.OrdersMasters.ToList();
        }

        public void UpdateItem(int ItemId, OrdersMasters NewData)
        {
            var SearchedItem = dB.OrdersMasters.Where(x => x.Id == ItemId).FirstOrDefault();
            SearchedItem.Bazy = NewData.Bazy;
            SearchedItem.Email = NewData.Email;
            SearchedItem.FullName = NewData.FullName;
            SearchedItem.Grad = NewData.Grad;
            SearchedItem.PassWord = NewData.PassWord;
            dB.SaveChanges();
        }
        public List<BuyOperation> GetOrderMasterOrders(string OrderMasterName)
        {
            var SearchedOrderMasterInfo = dB.OrdersMasters.Where(x => x.FullName == OrderMasterName).FirstOrDefault();
            return dB.BuyOperation.Where(x => x.OrderMasterID == SearchedOrderMasterInfo.Id).ToList();
        }
        public OrdersMasters SearchedOrderMasterInfo(string OrderMasterName)
        {
            return dB.OrdersMasters.Where(x => x.FullName == OrderMasterName).FirstOrDefault();
        }
        public bool NameAndPassWordVerification(string Name , string PassWord)
        {
            var SearchedMaster = dB.OrdersMasters.Where(x => x.FullName == Name || x.PassWord == PassWord).FirstOrDefault();
            if (SearchedMaster == null)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
       
    }
}