using BuyWebApplication.Models;
using ElMarket.Models;
using ElMarket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElMarket.Repo
{
    public class BuyOperationRepo : Generalinteface<BuyOperation>
    {
        private readonly ContextClass dB;

        public BuyOperationRepo(ContextClass DB)
        {
            dB = DB;
        }
        public void AddNewItem(BuyOperation NewItem)
        {
            dB.BuyOperation.Add(NewItem);
            dB.SaveChanges();
        }

        public void DeleteItem(int Id)
        {
            dB.BuyOperation.Remove(dB.BuyOperation.ToList().Where(x => x.OpeartionId == Id).FirstOrDefault());
            dB.SaveChanges();
        }

        public List<BuyOperation> GetAllData()
        {
            return dB.BuyOperation.ToList();

        }

        public void UpdateItem(int ItemId, BuyOperation NewData)
        {
            var SearchedUser = dB.BuyOperation.ToList().Where(x => x.OpeartionId == ItemId).FirstOrDefault();
            SearchedUser.Bill = NewData.Bill;
            SearchedUser.ProductId = NewData.ProductId;
            SearchedUser.ProductTime = NewData.ProductTime;
            SearchedUser.Quantity = NewData.Quantity;
            SearchedUser.UserId = NewData.UserId;
            dB.SaveChanges();
        }
        public int GetOperationsNumberByUserId(int UserId)
        {
            return dB.BuyOperation.ToList().Where(x => x.UserId == UserId).ToList().Count();
        }
        public List<BuyOperation> GetOperationsByUserUserName(string UserName)
        {
            var SearchedUser = dB.User.ToList().Where(x => x.UserName == UserName).FirstOrDefault();
            return dB.BuyOperation.ToList().Where(x => x.UserId == SearchedUser.UserId).ToList();
        }
        public List<CountryStatistics> GetUserCountryStatistics(int UserId)
        {
            List<CountryStatistics> StatisticsData = new List<CountryStatistics>() { 
                new CountryStatistics()
                {
                    Country = "Egypt",
                    Value = 0
                },
                new CountryStatistics()
                {
                    Country = "USA",
                    Value = 0
                },
                new CountryStatistics()
                {
                    Country = "China",
                    Value = 0
                },
                new CountryStatistics()
                {
                    Country = "Japan",
                    Value = 0
                },
                new CountryStatistics()
                {
                    Country = "Germany",
                    Value = 0
                },
                new CountryStatistics()
                {
                    Country = "UK",
                    Value = 0
                },
                new CountryStatistics()
                {
                    Country = "France",
                    Value = 0
                },
                new CountryStatistics()
                {
                    Country = "India",
                    Value = 0
                },
                new CountryStatistics()
                {
                    Country = "Spain",
                    Value = 0
                },
                new CountryStatistics()
                {
                    Country = "Russia",
                    Value = 0
                },
                new CountryStatistics()
                {
                    Country = "Sudia",
                    Value = 0
                },
                new CountryStatistics()
                {
                    Country = "Emarate",
                    Value = 0
                },
                new CountryStatistics()
                {
                    Country = "Canada",
                    Value = 0
                }
            };
            var UserProcesses = dB.BuyOperation.ToList().Where(x => x.UserId == UserId).ToList();
            foreach (var item in UserProcesses)
            {
                foreach (var StatisticsItem in StatisticsData)
                {
                    if (item.Country == StatisticsItem.Country)
                    {
                        StatisticsItem.Value += 1;
                    }
                }
            }
            return StatisticsData;
        }
        public BuyOperation SearchBuyOperatonByID(int Id)
        {
            return dB.BuyOperation.Where(x => x.OpeartionId == Id).FirstOrDefault();
        }
        public List<BuyOperation> GetAllNewOrders()
        {
            return dB.BuyOperation.Where(x => x.OrderMasterID == 0).ToList();
        }
    }
}