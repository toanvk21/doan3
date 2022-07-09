using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BaiTapLonMVC.Models;
using Model;

namespace BaiTapLonMVC.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Account"] != null)
            {
                return View();
            }

            return Redirect("/Home/Index");

        }
        
        public ActionResult Details()
        {
            if (Session["Account"] != null)
            {
                return View();
            }

            return Redirect("/Home/Index");

        }

        private DBContext db = new DBContext();

        public JsonResult GetAllCheckOut(string searchString)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<CheckOut> checkoutList = db.CheckOuts.ToList();
            if (string.IsNullOrEmpty(searchString))
            {
                return Json(checkoutList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var model = checkoutList.Where(x => x.ID.ToString().Contains(searchString)||
                                                    x.FirstName.ToLower().Contains(searchString.ToLower())||
                                                    x.LastName.ToLower().Contains(searchString.ToLower()));
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
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
        public JsonResult GetCheckOutByID(int IDCheckOut)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var model = db.CheckOuts.Find(IDCheckOut);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}