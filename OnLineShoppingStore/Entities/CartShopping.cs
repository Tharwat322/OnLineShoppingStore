using OnLineShoppingStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineShoppingStore.Entities
{
   public class CartShopping
    {
        public  Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
