using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BaiTapLonMVC.Models;
using Model;

namespace BaiTapLonMVC.Controllers
{
    public class HomeController : Controller
    {
        private DBContext db = new DBContext();

        public ActionResult Index()
        {
            return View();
        }

        //Danh mục sản phẩm
        public ActionResult NavCategory()
        {
            return View();
        }

        //Lấy danh mục sản phẩm
        public JsonResult GetAllCategory()
        {
            db.Configuration.ProxyCreationEnabled = false; //code chống lỗi khi dùng angular với entityfamerwork
            var model = db.Categories.Where(x => x.IsDeleted == false).ToList(); //lấy về danh sách danh mục

            return Json(model, JsonRequestBehavior.AllowGet);//trả về danh sách danh mục dưới dạng json
        }

        //Sản phẩm mới
        public ActionResult NewProduct()
        {
            return View();
        }

        //Lấy sản phẩm mới
        public JsonResult GetNewProduct()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.Products.Where(x => x.IsDeleted == false && x.IsStatus == true
                                                                    && x.IsNew == true).ToList();
            model.Reverse();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //Top bán chạy
        public ActionResult TopSelling()
        {
            return View();
        }

        //Lấy ds bán chạy
        public JsonResult GetTopSelling()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.Products.Where(x => x.IsDeleted == false && x.IsStatus == true
                                                                    && x.IsTopSeller == true).ToList();
            model.Reverse();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        //Carousel sản phẩm

        public ActionResult CarouselProduct()
        {
            return View();
        }

        //Ds hot deal
        public JsonResult GetHotDeal()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.Products.Where(x => x.IsDeleted == false && x.IsStatus == true
                                                                    && x.HotDeal == true).ToList();
            model.Reverse();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //DS sản phẩm
        public ActionResult AllProduct()
        {
            return View();
        }

        public JsonResult GetAllProduct()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.Products.Where(x => x.IsDeleted == false && x.IsStatus == true).ToList();
            model.Reverse();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //dS on top
        public JsonResult GetOnTop()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.Products.Where(x => x.IsDeleted == false && x.IsStatus == true
                                                                    && x.IsOnTop == true).ToList();
            model.Reverse();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //ds news
        public ActionResult AllNews()
        {
            return View();
        }

        public ActionResult DetailNews()
        {
            return View();
        }

        public JsonResult GetAllNews()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.News.ToList();
            model.Reverse();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNews(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            News news = db.News.Find(id);
            return Json(news, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductByCategory()
        {
            return View();
        }

        public JsonResult GetProductByCategory(string MetaName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.Products.Where(x => x.IsDeleted == false && x.Category1.MetaName.Equals(MetaName)).ToList();
            model.Reverse();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //Chi tiết sản phẩm
        public ActionResult DetailsProduct()
        {
            return View();
        }

        //Lấy sản phẩm bằng ID
        public JsonResult GetProduct(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Product Product = db.Products.Find(id);
            return Json(Product, JsonRequestBehavior.AllowGet);
        }

        //Lấy ds hình ảnh sản phẩm
        public JsonResult GetImageProduct(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.ImageProducts.Where(x => x.Product == id).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //Get ds sản phẩm liên quan(theo danh mục)
        public JsonResult GetRelatedProduct(int? category)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.Products.Where(x => x.Category == category && x.IsDeleted == false).ToList();
            model.Reverse();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //get ds bình luận sản phẩm
        public JsonResult GetReviewProduct(int? product)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.ReviewProducts.Where(x => x.Product == product).ToList();
            model.Reverse();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //bình luận
        public string ReviewProduct(ReviewProduct reviewProduct)
        {
            if (reviewProduct != null)
            {
                db.ReviewProducts.Add(reviewProduct);
                db.SaveChanges();
                return "Bình luận thành công";
            }
            else
            {
                return "Bình luận không thành công";
            }
        }

        //get ds bình luận tin tức
        public JsonResult GetCommentNews(int? news)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.CommentNews.Where(x => x.News == news).ToList();
            model.Reverse();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //bình luận
        public string CommentNews(CommentNew comment)
        {
            if (comment != null)
            {
                db.CommentNews.Add(comment);
                db.SaveChanges();
                return "Bình luận thành công";
            }
            else
            {
                return "Bình luận không thành công";
            }
        }
        //Giỏ hàng
        public ActionResult YourCart()
        {
            return View();
        }

        //Lấy giỏ hàng
        public JsonResult GetYourCarts()
        {
            List<Cart> cartList = (List<Cart>) Session["Cart"];
            return Json(cartList, JsonRequestBehavior.AllowGet);
        }

        //tính tổng tiền
        public int SubTotal()
        {

            List<Cart> cartList = (List<Cart>) Session["Cart"];
            int sum = 0;
            if (cartList != null)
            {
                foreach (Cart cart in cartList)
                {
                    sum += cart.Cost * cart.Quantity;
                }
            }
            

            return sum;
        }

        //thêm giỏ hàng
        public string AddCarts(int IDProduct)
        {
            Product product = db.Products.Find(IDProduct);

            if (Session["Cart"] == null)
            {
                List<Cart> cartList = new List<Cart>();
                Cart cart = new Cart(product.ID, product.Name, 1, product.Image, product.Cost.Value);
                cartList.Add(cart);
                Session["Cart"] = cartList;
            }
            else
            {
                List<Cart> cartList = (List<Cart>) Session["Cart"];
                int index = isExist(IDProduct);
                if (index != -1)
                {
                    cartList[index].Quantity++;
                }
                else
                {
                    Cart cart = new Cart(product.ID, product.Name, 1, product.Image, product.Cost.Value);
                    cartList.Add(cart);
                }

                Session["Cart"] = cartList;
            }

            return "Thành công";
        }

        //check tồn tại
        private int isExist(int id)
        {
            List<Cart> cart = (List<Cart>) Session["Cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].IdProduct == id)
                    return i;
            return -1;
        }

        //xóa sp khỏi giỏ hàng
        public string DeleteCart(int index)
        {
            List<Cart> cartList = (List<Cart>) Session["Cart"];
            cartList.RemoveAt(index);
            Session["Cart"] = cartList;
            return "Xóa thành công";
        }

        //Giỏ hàng
        public ActionResult ShoppingCart()
        {
            return View();
        }

        //cập nhật giỏ hàng
        public void UpdateCart(string index, string quantity)
        {
            List<Cart> cartList = (List<Cart>) Session["Cart"];
            cartList[int.Parse(index)].Quantity = int.Parse(quantity);
            Session["Cart"] = cartList;
        }

        //checkout
        public ActionResult CheckOut()
        {
            return View();
        }

        //thêm checkout
        public int AddCheckout(CheckOut checkOut)
        {
            if (checkOut != null)
            {
                db.CheckOuts.Add(checkOut);
                db.SaveChanges();
                AddOrder(checkOut.ID);
                return checkOut.ID;
            }
            else
            {
                return checkOut.ID;
            }
        }

        //thêm đặt hàng
        public void AddOrder(int CheckOut)
        {
            List<Cart> cartList = (List<Cart>) Session["Cart"];
            foreach (Cart cart in cartList)
            {
                Order order = new Order();
                order.Product = cart.IdProduct;
                order.Quantity = cart.Quantity;
                order.CheckOut = CheckOut;
                db.Orders.Add(order);
            }

            db.SaveChanges();
            List<Cart> carts = new List<Cart>();
            Session["Cart"] = carts;
        }

        //theo dõi đơn hàng
        public ActionResult OrderTracking()
        {
            return View();
        }

        //get giỏ hàng theo IDcheckout
        public JsonResult GetCartByID(int IDCheckOut)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Order> orders = db.Orders.Where(x => x.CheckOut == IDCheckOut).ToList();
            List<Cart> carts = new List<Cart>();
            foreach (Order order in orders)
            {
                Cart cart = new Cart();
                cart.NameProduct = db.Products.Find(order.Product).Name;
                cart.Cost = db.Products.Find(order.Product).Cost.Value;
                cart.Image = order.Product1.Image;
                cart.Quantity = order.Quantity.Value;
                carts.Add(cart);
            }

            return Json(carts, JsonRequestBehavior.AllowGet);
        }

        //get checkout theo id
        public JsonResult GetCheckOutByID(int IDCheckOut)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.CheckOuts.Find(IDCheckOut);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchBox()
        {
            return View();
        }

        public JsonResult Search(int? filter, string search)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (filter != null)
            {
                if (filter < 0)
                {
                    var model = db.Products.Where(x => x.Name.ToLower().Contains(search.ToLower()) ||
                                                       x.Brand1.Name.ToLower().Contains(search.ToLower()) ||
                                                       x.Category1.Name.ToLower().Contains(search.ToLower()) ||
                                                       x.Name.ToLower().Contains(search.ToLower())).ToList();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var model = db.Products.Where(x => (x.Name.ToLower().Contains(search.ToLower()) ||
                                                       x.Brand1.Name.ToLower().Contains(search.ToLower())) &&
                                                       x.Category == filter).ToList();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var model = db.Products.Where(x => x.Name.ToLower().Contains(search.ToLower()) ||
                                                   x.Brand1.Name.ToLower().Contains(search.ToLower()) ||
                                                   x.Category1.Name.ToLower().Contains(search.ToLower()) ||
                                                   x.Name.ToLower().Contains(search.ToLower())).ToList();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SearchProduct()
        {
            return View();
        }
        //Tracking
        public ActionResult Tracking()
        {
            return View();
        }
        
        public JsonResult GetAllChecking(string searchString)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<CheckOut> checkoutList = db.CheckOuts.ToList();
            if (string.IsNullOrEmpty(searchString)||searchString=="")
            {
                return null;
            }
            else
            {
                var model = checkoutList.Where(x => x.ID.ToString().Contains(searchString)||
                                                    x.FirstName.ToLower().Contains(searchString.ToLower())||
                                                    x.LastName.ToLower().Contains(searchString.ToLower()));
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        public JsonResult LoginAdmin(Account account)
        {
            db.Configuration.ProxyCreationEnabled = false;
            bool check = false;
            foreach (Account item in db.Accounts.ToList())
            {
                if (item.Username.Trim().Equals(account.Username) && item.Password.Trim().Equals(account.Password))
                {
                    check = true;
                    Session["Account"] = item;
                }
            }

            return Json(check, JsonRequestBehavior.AllowGet);
        }
    }
}