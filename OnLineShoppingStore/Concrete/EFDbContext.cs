using OnLineShoppingStore.Domain.Entities;
using OnLineShoppingStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineShoppingStore.Domain.Concrete
{
   public class EFDbContext:DbContext
    {
        public DbSet <Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
