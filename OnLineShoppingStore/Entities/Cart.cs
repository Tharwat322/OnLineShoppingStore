
using OnLineShoppingStore.Domain.Entities;
using OnLineShoppingStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineShoppingStore.Domain.Entities
{
   public class Cart
    {
        private List<CartShopping> ShoppingProducts = new List<CartShopping>();
        //methods
        public void AddItem(Product product , int quantity)
        {
            CartShopping products = ShoppingProducts.Where(p => p.Product == product).FirstOrDefault();
            if(products == null)
            {
                ShoppingProducts.Add(new CartShopping { Product = product, Quantity = quantity });
            }
            else
            {
                products.Quantity+= quantity;
                
            }
        }
        public void RemoveItem(Product product)
        {
            ShoppingProducts.RemoveAll(p => p.Product.ProductId == product.ProductId);
        }

        public decimal ComputeTotalPrice()
        {
            decimal result = ShoppingProducts.Sum(p => p.Product.Price * p.Quantity);
            return (result);
        }

        public IEnumerable<CartShopping> Shopping {
            get
            {
                return ShoppingProducts;
            }
        }

        public void Clear()
        {
            ShoppingProducts.Clear();
        }
    }
}
