using BuyWebApplication.Models;
using ElMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElMarket.Repo
{
    public class UserRepo : Generalinteface<User>
    {
        private readonly ContextClass dB;

        public UserRepo(ContextClass DB)
        {
            dB = DB;
        }
        public void AddNewItem(User NewItem)
        {
            dB.User.Add(NewItem);
            dB.SaveChanges();
        }

        public void DeleteItem(int Id)
        {
            dB.User.Remove(dB.User.ToList().Where(x => x.UserId == Id).FirstOrDefault());
            dB.SaveChanges();
        }

        public List<User> GetAllData()
        {
            return dB.User.ToList();
        }

        public void UpdateItem(int ItemId, User NewData)
        {
            var SearchedUser = dB.User.ToList().Where(x => x.UserId == ItemId).FirstOrDefault();
            SearchedUser.Address = NewData.Address;
            SearchedUser.Email = NewData.Email;
            SearchedUser.OperationsNumber = NewData.OperationsNumber;
            SearchedUser.PassWord = NewData.PassWord;
            SearchedUser.Phone = NewData.Phone;
            SearchedUser.Rank = NewData.Rank;
            SearchedUser.SecretKey = NewData.SecretKey;
            SearchedUser.UserName = NewData.UserName;
            dB.SaveChanges();
        }
        public string CheckUser(string UserName , string PassWord)
        {
            var SearchedUser = dB.User.ToList().Where(x => x.UserName == UserName && x.PassWord == PassWord).FirstOrDefault();
            if (SearchedUser == null)
            {
                var SearchedOrderMaster = dB.OrdersMasters.Where(x => x.FullName == UserName && x.PassWord == PassWord).FirstOrDefault();
                if (SearchedOrderMaster == null)
                {
                    return "None";
                }
                return "OrderManager";
            }
            return "Exist";
        }
        public User SearchUserByName(string UserName)
        {
            return dB.User.ToList().Where(x => x.UserName == UserName ).FirstOrDefault(); 
        }
        public User SearchUserById(int UserId)
        {
            return dB.User.ToList().Where(x => x.UserId == UserId).FirstOrDefault();
        }
    }
}