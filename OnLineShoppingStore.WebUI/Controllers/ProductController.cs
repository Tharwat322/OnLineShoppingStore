using OnLineShoppingStore.Abstract;
using OnLineShoppingStore.Domain.Entities;
using OnLineShoppingStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnLineShoppingStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private readonly IProductRepository repository;
        public int PageSize = 2;
        public ProductController(IProductRepository repos)
        {
            repository = repos;
        }

        public ActionResult List(string category , int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products.OrderBy(p => p.ProductId)
                .Where(p => category == null || p.Category == category)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() :
                                                    repository.Products.Where(c => c.Category == category).Count()
                },

                CurrentCategory = category
            };

            return View(model);
        }
    }
}