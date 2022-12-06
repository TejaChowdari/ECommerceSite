using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Linq;
using ShoppingCartMVC.Models;

namespace ShoppingCartMVC.Controllers
{
    public class AccountController : Controller
    {
        readonly dbOnlineStoreEntities db = new dbOnlineStoreEntities();

        #region user registration 
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(tblUser t)
        {
            tblUser u = new tblUser();
            if (ModelState.IsValid)
            {
                u.Name = t.Name;
                u.Email = t.Email;
                u.Password = t.Password;
                u.RoleType = 3;
                u.Mobile=t.Mobile;
                u.DNo = t.DNo;
                u.Street = t.Street;
                u.City = t.City;
                u.State = t.State;
                u.Pincode=t.Pincode;
                db.tblUsers.Add(u);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            else
            {
                TempData["msg"] = "Already Registered!!";
            }            

            return View();
        }
        #endregion

        #region user login

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tblUser t)
        {
            var query = db.tblUsers.SingleOrDefault(m => m.Email == t.Email && m.Password == t.Password);
            if (query != null)
            {
                if (query.RoleType == 1)
                {
                    Session["uid"] = query.UserId;
                    FormsAuthentication.SetAuthCookie(query.Email, false);
                    Session["User"] = query.Name;
                    return RedirectToAction("Index", "Home");
                }
                else if (query.RoleType == 2)
                {
                    Session["uid"] = query.UserId;
                    FormsAuthentication.SetAuthCookie(query.Email, false);
                    Session["User"] = query.Name;
                    return RedirectToAction("Index", "Home");
                }
                else if(query.RoleType == 3)
                {
                    Session["uid"] = query.UserId;
                    FormsAuthentication.SetAuthCookie(query.Email, false);
                    Session["User"] = query.Name;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["msg"] = "Invalid Username or Password";
            }

            return View();
        }
        #endregion

        #region logout 
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Start ReqtoSell
        public ActionResult ReqtoSell()
        {
            return View();
        }
        #endregion End ReqtoSell

        #region Start ConfirmAsSeller
        public ActionResult ConfirmAsSeller()
        {

            return View();
        }
        #endregion End ConfirmAsSeller

    }
}
