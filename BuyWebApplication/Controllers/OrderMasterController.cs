using BuyWebApplication.Models;
using ElMarket.Models;
using ElMarket.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElMarket.Controllers
{
    public class OrderMasterController : Controller
    {
        public OrderMasterRepo OrderFuns { get; }
        public BuyOperationRepo BuyOperationFuns { get; }
        public ProductRepo ProductFuns { get; }

        public OrderMasterController(OrderMasterRepo OrderFuns , BuyOperationRepo BuyOperationFuns , ProductRepo ProductFuns)
        {
            this.OrderFuns = OrderFuns;
            this.BuyOperationFuns = BuyOperationFuns;
            this.ProductFuns = ProductFuns;
        }

        // GET: OrderMaster
        public ActionResult ShowOrderMasterInfo(string UserName)
        {
            var OrderMasterBuyOperations = OrderFuns.GetOrderMasterOrders(UserName);
            ViewBag.UserName = UserName;
            return View(OrderMasterBuyOperations);
        }
        public ActionResult ShowProductsDeails(int ProductId , string UserName)
        {
            var ProductDetails = ProductFuns.SearchProductById(ProductId);
            ViewBag.UserName = UserName;
            return View(ProductDetails);
        }
        public ActionResult ShowProdile(string UserName)
        {
            var OrderMaster = OrderFuns.SearchedOrderMasterInfo(UserName);
            ViewBag.UserName = OrderMaster.FullName;
            return View(OrderMaster);
        }
        public ActionResult ChangeOrderState(string UserName , int OrderId ,bool State)
        {
            var SearchedOrder = BuyOperationFuns.SearchBuyOperatonByID(OrderId);
            SearchedOrder.DoneOrNot = State;
            BuyOperationFuns.UpdateItem(SearchedOrder.OpeartionId, SearchedOrder);
            return RedirectToAction(nameof(ShowOrderMasterInfo), new { UserName = UserName });
        }
    }
}