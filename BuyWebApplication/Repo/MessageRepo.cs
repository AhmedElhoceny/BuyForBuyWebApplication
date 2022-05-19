using BuyWebApplication.Models;
using ElMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElMarket.Repo
{
    public class MessageRepo : Generalinteface<Messages>
    {
        private readonly ContextClass dB;

        public MessageRepo(ContextClass DB)
        {
            dB = DB;
        }
        public void AddNewItem(Messages NewItem)
        {
            dB.Messages.Add(NewItem);
            dB.SaveChanges();
        }

        public void DeleteItem(int Id)
        {
            dB.Messages.Remove(dB.Messages.ToList().Where(x => x.id == Id).FirstOrDefault());
            dB.SaveChanges();
        }

        public List<Messages> GetAllData()
        {
            return dB.Messages.ToList();
        }

        public void UpdateItem(int ItemId, Messages NewData)
        {
            var SearchedItem = dB.Messages.ToList().Where(x => x.id == ItemId).FirstOrDefault();
            SearchedItem.Content = NewData.Content;
            SearchedItem.MessageTime = NewData.MessageTime;
            dB.SaveChanges();
        }
        public List<Messages> GetAllUserMessages(string UserName)
        {
            return dB.Messages.ToList().Where(x => x.To == UserName).ToList();
        }
        public Messages GetMessageByContent(string Content)
        {
            return dB.Messages.ToList().Where(x => x.Content == Content).FirstOrDefault();
        }
    }
}