using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCartMVC.Models;
using System.IO;
using System.Data;
namespace ShoppingCartMVC.Controllers
{
    public class ProductsController : Controller
    {
        readonly dbOnlineStoreEntities db = new dbOnlineStoreEntities();

        #region showing all products for admin 

        public ActionResult Index()
        {
            var query = db.getallproducts.ToList();
            return View(query);
        }
        #endregion

        #region products add for admin
        public ActionResult Create()
        {
            List<tblCategory> list = db.tblCategories.ToList();
            ViewBag.CatList = new SelectList(list, "CatId", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(tblProduct p , HttpPostedFileBase Image)
        {
            List<tblCategory> list = db.tblCategories.ToList();
            ViewBag.CatList = new SelectList(list, "CatId", "Name");            
            if (ModelState.IsValid)
            {
                tblProduct pro = new tblProduct
                {
                    Name = p.Name,
                    Description = p.Description,
                    Unit = p.Unit,
                    Image = Image.FileName.ToString(),
                    CatId = p.CatId
                };
                //image upload
                var folder = Server.MapPath("~/Uploads/");
                Image.SaveAs(Path.Combine(folder, Image.FileName.ToString()));
                db.tblProducts.Add(pro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["msg"] = "Product Not Upload";
            }
            return View();
        }
        #endregion

        #region edit products
        public ActionResult Edit(int id)
        {
            List<tblCategory> list = db.tblCategories.ToList();
            ViewBag.CatList = new SelectList(list, "CatId", "Name");
            var query = db.tblProducts.SingleOrDefault(m => m.ProID == id);
            return View(query);
        }
    
        [HttpPost]
        public ActionResult Edit(tblProduct p, HttpPostedFileBase Image)
        {
            List<tblCategory> list = db.tblCategories.ToList();
            ViewBag.CatList = new SelectList(list, "CatId", "Name");
            try
            {
                p.Image = Image.FileName.ToString();
                var folder = Server.MapPath("~/Uploads/");
                Image.SaveAs(Path.Combine(folder, Image.FileName.ToString()));
                db.Entry(p).State = (System.Data.Entity.EntityState)EntityState.Modified;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                TempData["msg"] = ex;
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region delete product 
        public ActionResult Delete(int id)
        {
            var query = db.tblProducts.SingleOrDefault(m => m.ProID == id); 
            db.tblProducts.Remove(query);                
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}