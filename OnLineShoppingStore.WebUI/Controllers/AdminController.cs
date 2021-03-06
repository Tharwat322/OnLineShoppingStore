﻿using OnLineShoppingStore.Abstract;
using OnLineShoppingStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnLineShoppingStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController( IProductRepository repos)
        {
            repository = repos;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View(repository.Products);
        }
        
        public ActionResult Create()
        {
            return View(new Product());
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been created", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult Delete (int productId)
        {
           Product deletedProduct = repository.DeleteProduct(productId);
            if(deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} has been deleted", deletedProduct.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(deletedProduct);
            }
            
        }
        public ActionResult Edit (int productId)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit (Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }
    }
}