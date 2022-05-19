using BuyWebApplication.Models;
using ElMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElMarket.Repo
{
    public class ProductRepo:Generalinteface<Product>
    {
        private readonly ContextClass dB;

        public ProductRepo(ContextClass DB)
        {
            dB = DB;
        }
        public void AddNewItem(Product NewItem)
        {
            var SearchedProduct = dB.Product.ToList().Where(x => x.ProductTitle == NewItem.ProductTitle).FirstOrDefault();
            if (SearchedProduct == null)
            {
                dB.Product.Add(NewItem);
            }
            dB.SaveChanges();
        }

        public void DeleteItem(int Id)
        {
            dB.Product.Remove(dB.Product.Where(x => x.UserId == Id).FirstOrDefault());
            dB.SaveChanges();
        }

        public List<Product> GetAllData()
        {
            return dB.Product.ToList();
        }

        public void UpdateItem(int ItemId, Product NewData)
        {
            var SearchedUser = dB.Product.ToList().Where(x => x.ProductId == ItemId).FirstOrDefault();
            SearchedUser.Category = NewData.Category;
            SearchedUser.Price = NewData.Price;
            SearchedUser.ProductTitle = NewData.ProductTitle;
            SearchedUser.UserId = NewData.UserId;
            dB.SaveChanges();
        }

        public Product SearchProductByName(string ProductName)
        {
            return dB.Product.ToList().Where(x => x.ProductTitle == ProductName).FirstOrDefault();
        }
        public Product SearchProductById(int ProductId)
        {
            return dB.Product.ToList().Where(x => x.ProductId == ProductId).FirstOrDefault();
        }
        public List<Product> GetUserProductsByCategory(string UserName , string Category)
        {
            if (Category == "null")
            {
                return new List<Product>();
            }
            var SearchedUser = dB.User.ToList().Where(x => x.UserName == UserName).FirstOrDefault();
            return dB.Product.ToList().Where(x => x.Category == Category && x.UserId == SearchedUser.UserId).ToList();
        }
        public void DeleteProductByProductTitle(string productTitle)
        {
            var SearchedProduct = dB.Product.ToList().Where(x => x.ProductTitle == productTitle).FirstOrDefault();
            dB.Product.Remove(SearchedProduct);
            dB.SaveChanges();
        }
        public List<Product> SearchProductsByCategories(int pageNumber, string Category)
        {
            if (Category == "'All'" || Category == "All" )
            {
                if (pageNumber == 1)
                {
                    return dB.Product.Take(6).ToList();
                }
                else
                {
                    return dB.Product.ToList().Skip(6 * (pageNumber - 1)).Take(6).ToList();
                }
            }
            else
            {
                if (pageNumber == 1)
                {
                    return dB.Product.ToList().Where(x => x.Category == Category).Take(6).ToList();
                }
                else
                {
                    return dB.Product.ToList().Where(x => x.Category == Category).Skip(6 * (pageNumber - 1)).Take(6).ToList();
                }
            }
            
        } 
    }
}