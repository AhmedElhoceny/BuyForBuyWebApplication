using BuyWebApplication.Models;
using ElMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElMarket.Repo
{
    public class CategoriesRepo : Generalinteface<Categories>
    {
        private readonly ContextClass dB;

        public CategoriesRepo(ContextClass DB)
        {
            dB = DB;
        }
        public void AddNewItem(Categories NewItem)
        {
            dB.Categories.Add(NewItem);
            dB.SaveChanges();
        }

        public void DeleteItem(int Id)
        {
            var SearchedCategory = dB.Categories.Where(x => x.Categories_Id == Id).FirstOrDefault();
            dB.Categories.Remove(SearchedCategory);
            dB.SaveChanges();
        }

        public List<Categories> GetAllData()
        {
            return dB.Categories.ToList();
        }

        public void UpdateItem(int ItemId, Categories NewData)
        {
            var SearchedCategory = dB.Categories.Where(x => x.Categories_Id == ItemId).FirstOrDefault();
            SearchedCategory.Title = NewData.Title;
            dB.SaveChanges();
        }
        public string SearchData(int Id)
        {
            return dB.Categories.Where(x => x.Categories_Id == Id).FirstOrDefault().Title;
        }
    }
}