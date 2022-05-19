using BuyWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElMarket.Repo
{
    interface Generalinteface<T>
    {
        List<T> GetAllData();
        void DeleteItem(int Id );
        void AddNewItem(T NewItem );
        void UpdateItem(int ItemId, T NewData);
    }
}
