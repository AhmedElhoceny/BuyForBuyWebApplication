using BuyWebApplication.Models;
using ElMarket.Models;
using ElMarket.Repo;
using ElMarket.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ElMarket.Controllers
{
    public class UserController : Controller
    {
        public UserRepo UserFuns;
        public ProductRepo ProductFuns;
        public BuyOperationRepo BuyFun;
        public MessageRepo MessagesFun;
        public IHostingEnvironment hosting;
        public OrderMasterRepo OrderMasterFuns { get; set; }
        public CategoriesRepo CategoriesRepo { get; }

        public UserController(UserRepo UserFuns , ProductRepo ProductFuns , BuyOperationRepo BuyFun , MessageRepo MessagesFun , OrderMasterRepo OrderMasterFuns , CategoriesRepo CategoriesRepo)
        {
            this.UserFuns = UserFuns;
            this.ProductFuns = ProductFuns;
            this.BuyFun = BuyFun;
            this.MessagesFun = MessagesFun;
            this.OrderMasterFuns = OrderMasterFuns;
            this.CategoriesRepo = CategoriesRepo;

        }
        // GET: User
        public ActionResult ShowUserIndex(string UserName)
        {
            var SearchedUser = UserFuns.SearchUserByName(UserName);
            List<CountryStatistics> CountryAnalytics = new List<CountryStatistics>();
            ViewBag.UserId = SearchedUser.UserId;
            ViewBag.UserName = SearchedUser.UserName;
            if (SearchedUser != null)
            {
                ViewBag.Operation = BuyFun.GetOperationsNumberByUserId(SearchedUser.UserId);
                ViewBag.Grade = SearchedUser.Rank;
                CountryAnalytics = BuyFun.GetUserCountryStatistics(SearchedUser.UserId);
                return View(CountryAnalytics);
            }
            else
            {
                return Redirect("/Home/LogIn");
            }
        }
        public ActionResult AddNewProductView(int UseId)
        {
            ViewBag.UserId = UseId;
            var AllCategories = CategoriesRepo.GetAllData();
            return View(AllCategories);
        }
        [HttpPost]
        public async Task<ActionResult> AddNewProductPostAsync(string ProductName, string ProductCategory, int Price, int UserId , IFormFile ProductImage , string ProductDiscription)
        {

            var SearchedUser = UserFuns.SearchUserById(UserId);
            if (ProductImage != null)
            {
                try
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Attachments");
                    var FileName = ProductImage.FileName;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await ProductImage.CopyToAsync(stream);
                    }
                    ProductFuns.AddNewItem(new Product()
                    {
                        Category = ProductCategory,
                        Price = Price,
                        ProductTitle = ProductName,
                        UserId = UserId,
                        Discription = ProductDiscription,
                        Image = FileName
                    });
                }
                catch (Exception)
                {
                    ProductFuns.AddNewItem(new Product()
                    {
                        Category = ProductCategory,
                        Price = Price,
                        ProductTitle = ProductName,
                        UserId = UserId,
                        Discription = ProductDiscription,
                        Image = "Product.jpg"
                    });
                }
            }
            else
            {
                ProductFuns.AddNewItem(new Product()
                {
                    Category = ProductCategory,
                    Price = Price,
                    ProductTitle = ProductName,
                    UserId = UserId,
                    Discription = ProductDiscription,
                    Image = "Product.jpg"
                });
            }
            return RedirectToAction(nameof(ShowUserIndex) , new { UserName = SearchedUser.UserName });
        }
        public ActionResult ShowAllBuyProcesses(string UserName)
        {
            var SearchedUser = UserFuns.SearchUserByName(UserName);
            ViewBag.UserId = SearchedUser.UserId;
            ViewBag.UserName = SearchedUser.UserName;
            var TotalMoney = 0;
            List<BuyOperation> ClientOperations = BuyFun.GetOperationsByUserUserName(UserName);
            foreach (var BuyProcessItem in ClientOperations)
            {
                TotalMoney += BuyProcessItem.Bill;
            }
            ViewBag.TotalMoney = TotalMoney;
            return View(ClientOperations);
        }
        public string DeleteOperationInfo(int OperationId)
        {
            try
            {
                BuyFun.DeleteItem(OperationId);

                return "Done";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public ActionResult ShowClientMessages(string UserName)
        {
            var UserMessages = MessagesFun.GetAllUserMessages(UserName);
            var SearchedUser = UserFuns.SearchUserByName(UserName);
            ViewBag.UserId = SearchedUser.UserId;
            ViewBag.UserName = SearchedUser.UserName;
            return View(UserMessages);
        }
        public string DeleteMessage(int MessageId)
        {
            try
            {
                MessagesFun.DeleteItem(MessageId);
                return "Done";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public ActionResult ShowUserProduct(string UserName , string Category)
        {
            var ModelData = new ListUserProductsViewModel();
            var SearchedUser = UserFuns.SearchUserByName(UserName);
            ViewBag.UserId = SearchedUser.UserId;
            ViewBag.UserName = SearchedUser.UserName;
            ModelData.UserProducts = ProductFuns.GetUserProductsByCategory(UserName, Category);
            ModelData.AllCategories = CategoriesRepo.GetAllData();
            ViewBag.UserName = UserName;
            return View(ModelData);
        }
        public ActionResult DeleteProduct(string ProductTitle , string UserName)
        {
            ProductFuns.DeleteProductByProductTitle(ProductTitle);

            return RedirectToAction(nameof(ShowUserProduct), new { UserName = UserName, Category = "Clothes" });
        }
        public ActionResult EditProductView(string ProductTitle ,string UserName)
        {
            var SearchedProduct = ProductFuns.SearchProductByName(ProductTitle);
            ViewBag.UserName = UserName;
            ViewBag.OldTitle = ProductTitle;
            var AllCategories = CategoriesRepo.GetAllData();
            return View(AllCategories);
        }
        public string GetProductData(string ProductTitle)
        {
            var SearchedProduct = ProductFuns.SearchProductByName(ProductTitle);
            string jsonData = JsonConvert.SerializeObject(SearchedProduct);
            return jsonData;
        }
        public string EditProductPost(IFormFile Image  , int ProductPrice , string ProductDescription , string ProductCategory , string UserName , string ProductOldTitle)
        {
            var SearchedProduct = ProductFuns.SearchProductByName(ProductOldTitle);
            try
            {
                if (Image != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Attachments");
                    var FileName = Image.FileName;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        Image.CopyToAsync(stream);
                    }

                    SearchedProduct.Image = FileName;
                }
            }
            catch (Exception)
            {
                SearchedProduct.Image = "Product.jpg";
            }
            

            if (ProductPrice != 0)
            {
                SearchedProduct.Price = ProductPrice;
            }
            if (ProductDescription != "" && ProductDescription != null)
            {
                SearchedProduct.Discription = ProductDescription;
            }
            if (ProductCategory != "" && ProductCategory != null)
            {
                SearchedProduct.Category = ProductCategory;
            }
            ProductFuns.UpdateItem(SearchedProduct.ProductId, SearchedProduct);

            return "Done";
        }

        //*******************************OrderManagersSection*****************************

        public ActionResult ShowOrderMasters(string UserName)
        {
            var SearchedUser = UserFuns.SearchUserByName(UserName);
            ViewBag.UserId = SearchedUser.UserId;
            ViewBag.UserName = SearchedUser.UserName;
            var AllOrderMasters = OrderMasterFuns.GetAllData();
            return View(AllOrderMasters);
        }
        [HttpGet]
        public ActionResult Profile(string UserName)
        {
            var SearchedUser = UserFuns.SearchUserByName(UserName);
            ViewBag.UserId = SearchedUser.UserId;
            ViewBag.UserName = SearchedUser.UserName;
            return View(SearchedUser);
        }
        [HttpPost]
        public ActionResult ProfilePost(UserViewModel FormData)
        {
            var SearchedUserId = UserFuns.SearchUserByName(FormData.UserName);
            SearchedUserId.Address = FormData.Address;
            SearchedUserId.Email = FormData.Email;
            SearchedUserId.Phone = FormData.Phone;
            SearchedUserId.SecretKey = FormData.SecretKey;
            UserFuns.UpdateItem(SearchedUserId.UserId, SearchedUserId);

            return RedirectToAction(nameof(Profile) , new { UserName = FormData.UserName});
        }
    }
}