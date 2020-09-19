using OnLineShoppingStore.Abstract;
using OnLineShoppingStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnLineShoppingStore.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAuthentication _authentication;
        // inject dependencies
        public AccountController(IAuthentication authentication)
        {
            _authentication = authentication;
        }
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login( LoginViewModel model , string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authentication.Authenticate(model.UserName,model.PassWord))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return Redirect(ReturnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect user name");
                    return View();
                }
                
            }
            else
            {                
                return View();
            }
            
        }
        
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Admin");
        }
    }
}