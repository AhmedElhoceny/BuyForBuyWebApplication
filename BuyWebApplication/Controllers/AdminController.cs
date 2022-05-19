using BuyWebApplication.Models;
using ElMarket.Models;
using ElMarket.Repo;
using ElMarket.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElMarket.Controllers
{
    public class AdminController : Controller
    {
        public UserRepo UserFuns { get; }
        public OrderMasterRepo OrderMasterFuns { get; }
        public BuyOperationRepo BuyOperationFuns { get; }
        public CategoriesRepo CategoriesRepo { get; }

        public AdminController(BuyOperationRepo BuyOperationFuns , CategoriesRepo CategoriesRepo , OrderMasterRepo OrderMasterFuns , UserRepo UserFuns)
        {
            this.UserFuns = UserFuns;
            this.OrderMasterFuns = OrderMasterFuns;
            this.BuyOperationFuns = BuyOperationFuns;
            this.CategoriesRepo = CategoriesRepo;
        }
        
        // GET: Admin
        public ActionResult WelcomeAdmin()
        {
            return View();
        }
        public ActionResult ShowClients()
        {
            var OurClients = UserFuns.GetAllData();
            return View(OurClients);
        }
        public ActionResult ShowOrderMasters()
        {
            var OrderMasters = OrderMasterFuns.GetAllData();
            return View(OrderMasters);
        }
        public ActionResult AddOrderMasters(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddNewOrderMaster(AdminViewModel FormData)
        {
            if (FormData.PassWord == FormData.ConfirmPassWord && FormData.Email != null && FormData.FullName != null )
            {
                if (OrderMasterFuns.NameAndPassWordVerification(FormData.FullName , FormData.PassWord ))
                {
                    OrderMasterFuns.AddNewItem(new OrdersMasters()
                    {
                        Bazy = false,
                        Email = FormData.Email,
                        FullName = FormData.FullName,
                        Grad = 0,
                        PassWord = FormData.PassWord
                    });
                    
                    return RedirectToAction(nameof(ShowOrderMasters));
                }
                else
                {
                    return RedirectToAction(nameof(AddOrderMasters), new { message = "Existing Name or PassWord :)" });
                }
            }
            else
            {
                return RedirectToAction(nameof(AddOrderMasters) , new { message = "Failed :(" });
            }
        }
        public ActionResult RemoveClient(int ID)
        {
            UserFuns.DeleteItem(ID);
          
            return RedirectToAction(nameof(ShowClients));
        }
        public ActionResult RemoveOrderMaster(int ID)
        {
            OrderMasterFuns.DeleteItem(ID);
            
            return RedirectToAction(nameof(ShowOrderMasters));
        }
        public ActionResult ShowNewOrder()
        {
            var AllNewBuyOperations = BuyOperationFuns.GetAllNewOrders();
            return View(AllNewBuyOperations);
           
        }
        public ActionResult SendOrderToMaster(int OrderMasterId , int OrderId)
        {
            var SearchedOrder = BuyOperationFuns.SearchBuyOperatonByID(OrderId);
            SearchedOrder.OrderMasterID = OrderMasterId;
            BuyOperationFuns.UpdateItem(OrderId, SearchedOrder);
            
            return RedirectToAction(nameof(ShowNewOrder));
        }
        public ActionResult Categories() {

            var AllCategories = CategoriesRepo.GetAllData();
            return View(AllCategories);
        }
        [HttpPost]
        public ActionResult EditCategory( Categories NewData=null)
        {

            if ( NewData.Categories_Id == 0)
            {
                CategoriesRepo.AddNewItem(NewData);

            }
            else
            {
                CategoriesRepo.UpdateItem(NewData.Categories_Id, NewData);
            }

            return RedirectToAction(nameof(Categories));
        }
        public string GetCategoryData(int Id)
        {
            return CategoriesRepo.SearchData(Id);
        }
        public ActionResult DeleteCategory(int Id=0)
        {
            if (Id == 0)
            {
                return RedirectToAction(nameof(Categories));
            }
            CategoriesRepo.DeleteItem(Id);
            return RedirectToAction(nameof(Categories));
        }
    }
}