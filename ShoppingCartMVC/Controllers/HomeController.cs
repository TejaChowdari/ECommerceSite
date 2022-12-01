using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCartMVC.Models;
namespace ShoppingCartMVC.Controllers
{
    public class HomeController : Controller
    {
        /* Database Connection  */
        readonly dbOnlineStoreEntities db = new dbOnlineStoreEntities();

        /* Add to Cart List use */
        readonly List<Cart> li = new List<Cart>();

        #region home page in showing all products 

        public ActionResult Index()
        {

            if (TempData["cart"] != null)
            {
                int x = 0;

                List<Cart> li2 = TempData["cart"] as List<Cart>;
                foreach (var item in li2)
                {
                    x += item.Bill;

                }
                TempData["total"] = x;
                TempData["item_count"] = li2.Count();
            }
            TempData.Keep();

            var query = db.tblProducts.ToList();
            return View(query);
        }

        #endregion

        #region add to cart
        public ActionResult AddtoCart(int id)
        {
            var query =  db.tblProducts.Where(x => x.ProID == id).SingleOrDefault();
            return View(query);
        }

        [HttpPost]
        public ActionResult AddtoCart(int id,int qty)
        {
           tblProduct p = db.tblProducts.Where(x => x.ProID == id).SingleOrDefault();
            Cart c = new Cart
            {
                Proid = id,
                Image = p.Image,
                Proname = p.Name,
                Price = Convert.ToInt32(p.Unit),
                Qty = Convert.ToInt32(qty)
            };
            c.Bill = c.Price * c.Qty;
           if (TempData["cart"] == null)
           {
               li.Add(c);
               TempData["cart"] = li;
           }
           else
           {
               List<Cart> li2 = TempData["cart"] as List<Cart>;
               int flag = 0;
               foreach (var item in li2)
               {
                   if (item.Proid == c.Proid)
                   {
                       item.Qty += c.Qty;
                       item.Bill += c.Bill;
                       flag = 1;
                   }

               }
               if (flag == 0)
               {
                   li2.Add(c);
                   //new item
               }
               TempData["cart"] = li2;
           }      
           TempData.Keep();          
           return RedirectToAction("Index");
        }
        #endregion

        #region remove cart item
        public ActionResult Remove(int? id)
        {
            if (TempData["cart"] == null)
            {
                TempData.Remove("total");
                TempData.Remove("cart");
            }
            else
            {
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                Cart c = li2.Where(x => x.Proid == id).SingleOrDefault();
                li2.Remove(c);
                int s = 0;
                foreach (var item in li2)
                {
                    s += item.Bill;
                }
                TempData["total"] = s;
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region checkout code
        public ActionResult Checkout()
        {
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(string contact,string address)
        {
            if (ModelState.IsValid)
            {
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                tblInvoice iv = new tblInvoice
                {
                    UserId = Convert.ToInt32(Session["uid"].ToString()),
                    InvoiceDate = System.DateTime.Now,
                    Bill = (int)TempData["total"],
                    Payment = "cash"
                };
                db.tblInvoices.Add(iv);
                db.SaveChanges();
                //order book
                foreach (var item in li2)
                {
                    tblOrder od = new tblOrder
                    {
                        ProID = item.Proid,
                        Contact = contact,
                        Address = address,
                        OrderDate = System.DateTime.Now,
                        InvoiceId = iv.InvoiceId,
                        Qty = item.Qty,
                        Unit = item.Price,
                        Total = item.Bill
                    };
                    db.tblOrders.Add(od);
                    db.SaveChanges();
                }
                TempData.Remove("total");
                TempData.Remove("cart");
               // TempData["msg"] = "Order Book Successfully!!";
                return RedirectToAction("Index");
            }
            TempData.Keep();
            return View();
        }
        #endregion


        #region all orders for admin 

        public ActionResult GetAllOrderDetail()
        {
            var query = db.getallorders.ToList();
            return View(query);
        }

        #endregion

        #region  confirm order by admin

        public ActionResult ConfirmOrder(int InvoiceId)
        {
            var query = db.getallorders.SingleOrDefault(m=>m.InvoiceId == InvoiceId);
            return View(query);
        }

        [HttpPost]
        public ActionResult ConfirmOrder(getallorder o)
        {
            tblInvoice inv = new tblInvoice()
            {
                InvoiceId = o.InvoiceId,
                UserId = o.UserId,
                Bill = o.Bill,
                Payment = o.Payment,
                InvoiceDate = o.InvoiceDate,
                Status = 1,
            };

         
          
            db.Entry(inv).State = EntityState.Modified;
            db.SaveChanges();

            return View();
               
        }

        #endregion

        #region orders for only user

        public ActionResult OrderDetail(int id)
        {
            var query = db.getallorderusers.Where(m => m.UserId == id).ToList();
            return View(query);
        }

         #endregion


        #region  get all users 

        public ActionResult GetAllUser()
        {
            var query = db.tblUsers.ToList();
            return View(query);
        }

        #endregion



        #region invoice for  user

        public ActionResult Invoice(int id)
        {
            var query = db.user_invoices.Where(m => m.InvoiceId == id).ToList();
            return View(query);
        }

        #endregion

    }
}