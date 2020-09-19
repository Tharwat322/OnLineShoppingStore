using OnLineShoppingStore.Abstract;
using OnLineShoppingStore.Domain;
using OnLineShoppingStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineShoppingStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        // object of context
        private readonly EFDbContext context = new EFDbContext();
        // property to get products
        public IEnumerable<Product> Products
        {
            get
            {
                return context.Products;
            }
        }

        public Product DeleteProduct(int productId)
        {
            Product product = context.Products.Find(productId);
            if(product!= null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return product;
        }

        public void SaveProduct(Product product)
        {
            if(product.ProductId == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product entryProduct = context.Products.Find(product.ProductId);
                if(entryProduct!= null)
                {
                    entryProduct.ProductId = product.ProductId;
                    entryProduct.Name = product.Name;
                    entryProduct.Price = product.Price;
                    entryProduct.Category = product.Category;
                }
            }
            context.SaveChanges();
        }
    }
}
