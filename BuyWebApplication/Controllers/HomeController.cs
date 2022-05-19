using BuyWebApplication.Models;
using ElMarket.Hubs;
using ElMarket.Models;
using ElMarket.Repo;
using ElMarket.ViewModels;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ElMarket.Controllers
{
    public class HomeController : Controller
    {

        public UserRepo UserFuns;
        public ProductRepo ProductFuns;
        public BuyOperationRepo BuyFun;
        public MessageRepo MessageFun;
        private readonly UserManager<IdentityUser> userManage;
        private readonly SignInManager<IdentityUser> signManage;

        public CategoriesRepo CategoriesFuns { get; }

        public HomeController(UserRepo UserFuns , ProductRepo ProductFuns ,BuyOperationRepo BuyFun , MessageRepo MessageFun , CategoriesRepo CategoriesFuns )
        {
            this.UserFuns = UserFuns;
            this.ProductFuns = ProductFuns;
            this.BuyFun = BuyFun;
            this.MessageFun = MessageFun;
            this.CategoriesFuns = CategoriesFuns;
        }

        public ActionResult Index()
        {

            List<Product> SomeProducts = ProductFuns.GetAllData().Take(3).ToList();

            return View(SomeProducts);
        }

        public ActionResult LogIn()
        {
            return View();
        }
        public string LogInPost(LogInViewModel CheckedUserData)
        {
            if (CheckedUserData.UserName == "Admin" && CheckedUserData.PassWord == "Admin")
            {
                return "Admin";
            }
            var Result = UserFuns.CheckUser(CheckedUserData.UserName, CheckedUserData.PassWord);
            return Result;
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public string SignUpPost(SignUpUserViewModel NewUser)
        {
            var Flag = 1;
            try
            {
                var AllUsers = UserFuns.GetAllData();
                foreach (var UserItem in AllUsers)
                {
                    if (UserItem.UserName == NewUser.UserName || UserItem.Email == NewUser.Email || NewUser.password != NewUser.ConfirmPassWord)
                    {
                        Flag = 0;
                    }
                }
                if (Flag == 1)
                {
                    UserFuns.AddNewItem(new User()
                    {
                        Address = null,
                        Email = NewUser.Email,
                        OperationsNumber = 0,
                        PassWord = NewUser.password,
                        Phone = NewUser.Mobile,
                        SecretKey = "",
                        Rank = 0,
                        UserName = NewUser.UserName
                    });

                    return "Done";
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception)
            {
                return "Failed";
            }
            
        }
        public ActionResult ShowProducts(int pageNumber , string Category)
        {
            ListUserProductsViewModel ModelData = new ListUserProductsViewModel();
            ModelData.UserProducts = new List<Product>();
            ModelData.UserProducts = ProductFuns.SearchProductsByCategories(pageNumber, Category);
            ViewBag.PaggingNumber = ProductFuns.GetAllData().Count / 6;
            ViewBag.CategoryTitle = Category;
            if (ProductFuns.GetAllData().Count % 6 != 0)
            {
                ViewBag.PaggingNumber += 1;
            }
            ModelData.AllCategories = CategoriesFuns.GetAllData();
            return View(ModelData);
        }
        public ActionResult AboutUs()
        {
            List<User> OurClients = UserFuns.GetAllData().Take(6).ToList();
            return View(OurClients);
        }
        public ActionResult BuyProductProcess(string ProductTitle)
        {
            var SearchedProduct = ProductFuns.SearchProductByName(ProductTitle);
            var SeachedUser = UserFuns.SearchUserById(SearchedProduct.UserId);
            ViewBag.UserName = SeachedUser.UserName;
            ViewBag.secretKey = SeachedUser.SecretKey;
            ViewBag.PayPal = "https://www.paypal.com/sdk/js?client-id=" + SeachedUser.SecretKey + "&enable-funding=venmo&currency=USD";
            return View(SearchedProduct);
        }
        public string ProcessIsDone(string ProductTitle , int Bill , string Country , int Quantity , string Address , string OrderPhone)
        {
            var SearchedProduct = ProductFuns.SearchProductByName(ProductTitle);
            BuyFun.AddNewItem(new BuyOperation()
            {
                Bill = Bill,
                Country = Country,
                ProductId = SearchedProduct.ProductId,
                ProductTime = DateTime.Now,
                Quantity = Quantity,
                UserId = SearchedProduct.UserId,
                Address = Address,
                PhoneNumber = OrderPhone,
                type = "Online"
            });

            return "Done";
        }
        public string CashProcessIsDone(string ProductTitle, int Bill, string Country, int Quantity, string Address, string OrderPhone)
        {

            var SearchedProduct = ProductFuns.SearchProductByName(ProductTitle);
            BuyFun.AddNewItem(new BuyOperation()
            {
                Bill = Bill,
                Country = Country,
                ProductId = SearchedProduct.ProductId,
                ProductTime = DateTime.Now,
                Quantity = Quantity,
                UserId = SearchedProduct.UserId,
                Address = Address,
                PhoneNumber = OrderPhone,
                type = "Cash"
            });

            return "Done";
        }
        public string AddMessageToProduct(string MessageContent , string MessageFrom , string ProductTitle)
        {
            var SearchedProduct = ProductFuns.SearchProductByName(ProductTitle);
            var SearchedUser = UserFuns.SearchUserById(SearchedProduct.UserId);
            MessageFun.AddNewItem(new Messages()
            {
                Content = MessageContent,
                From = MessageFrom,
                MessageTime = DateTime.Now,
                To = SearchedUser.UserName
            });

            return "Done";
        }
    }
}