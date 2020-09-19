using OnLineShoppingStore.Abstract;
using OnLineShoppingStore.Domain.Entities;
using OnLineShoppingStore.Entities;
using OnLineShoppingStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnLineShoppingStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController(IProductRepository repos, IOrderProcessor processor)
        {
            repository = repos;
            orderProcessor = processor;
        }

        public ViewResult Index( Cart cart, string returnUrl)
        {
           return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        public PartialViewResult Summary( Cart cart)
        {
            return PartialView(cart);
        }
      
        public ViewResult CheckOut()
        {
            return View ( new ShippingDetails());
        }
        [HttpPost]
        public ViewResult CheckOut(Cart cart, ShippingDetails shippingDetails)
        {
            if( cart.Shopping.Count() == 0)
            {
                ModelState.AddModelError("","Sorry , Your Cart is empty");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessorOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        } 
        //public ViewResult CheckOut()
        //{
        //    return View( new ShippingDetails());
        //}
        // method to Add Product
        public RedirectToRouteResult AddToCart(Cart cart, int productId , string ReturnUrl)
        {
            Product product = repository.Products
                .Where(p => p.ProductId == productId)
                .FirstOrDefault();
            if( product != null)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { ReturnUrl });
        }
        // store Cart in Session to use it in Adding Product to cart Incase of null
       
       
        public RedirectToRouteResult RemoveFromCart( Cart cart,int productId , string  ReturnUrl)
        {
            Product product = repository.Products.Where(p => p.ProductId == productId).FirstOrDefault();
            if( product != null)
            {
                cart.RemoveItem(product);
            }
            return RedirectToAction("Index", new { ReturnUrl });
        }
        //private Cart GetCart()
        //{
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart == null)
        //    {
        //        cart = new Cart();
        //        Session["cart"] = cart;
        //    }
        //    return cart;
        //}
    }
}