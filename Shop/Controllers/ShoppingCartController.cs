using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        private int isExisting(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.Id == id)
                    return i;
            return -1;
        }

        public ActionResult ViewCart()
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
               
                Session["cart"] = cart;
            }
            return View("Cart");
        }


        public ActionResult AddToCart(int id)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(db.products.Find(id), 1));
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                    cart.Add(new Item(db.products.Find(id), 1));
                else
                    cart[index].Quantity++;

                Session["cart"] = cart;
            }
            return View("Cart");
        }

        public ActionResult Delete(int id)
        {
            int index = isExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
            if (index >= 2)
                cart[index].Quantity--;
            else
                cart.RemoveAt(index);
            Session["cart"] = cart;
            return View("Cart");
        }

        public ActionResult Update(FormCollection fc)
        {
            string[] quantities = fc.GetValues("quantity");
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                cart[i].Quantity = Convert.ToInt32(quantities[i]);
            Session["cart"] = cart;
            return View("Cart");
        }


        //Order 
        public ActionResult CheckOut(int? id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            Session["cart"] = cart;

            return View("CheckOut");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveOrder(FormCollection fc)
        {
            decimal orderTotal = 0;
            List<Item> cart = (List<Item>)Session["cart"];

            //Save Order
            Order order = new Order();
            order.OrderDate = DateTime.Now;
            order.FirstName = fc["FirstName"];
            order.LastName = fc["LastName"];
            order.Email = fc["Email"];
            order.Phone = fc["Phone"];
            order.Address = fc["Address"];
            order.City = fc["City"];
            order.State = fc["State"];
            order.PostalCode = fc["PostalCode"];
            order.Country = fc["Country"];

            db.Orders.Add(order);
            db.SaveChanges();


            //Save OrderDetail
            foreach (Item item in cart)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Quantity * item.Product.Price);

                db.OrderDetails.Add(orderDetail);

            }
            order.Total = orderTotal;
            db.SaveChanges();

            Session.Remove("cart");
            return RedirectToAction("Thanks", new { id = order.OrderId });
        }

        public ActionResult Thanks(int? id)
        {
            return View(id);
        }
       

        [ChildActionOnly]
        public ActionResult CartSummary()
        {

            int? orderCount = 0;
            List<Item> cart = (List<Item>)Session["cart"];
            if (cart != null)
            {
                
                    orderCount += cart.Count;
                
            }
            else
            {
                orderCount = 0;
            }

            ViewData["CartCount"] = orderCount;
            return PartialView("CartSummary");


        }

    }

}