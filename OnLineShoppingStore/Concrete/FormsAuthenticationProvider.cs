using OnLineShoppingStore.Abstract;
using OnLineShoppingStore.Domain.Concrete;
using OnLineShoppingStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineShoppingStore.Concrete
{
   public class FormsAuthenticationProvider : IAuthentication
    {
        private readonly EFDbContext context = new EFDbContext();
        public bool Authenticate(string username, string password)
        {
            var result = context.Users.FirstOrDefault(c => c.UserId == username && c.PassWord == password);
            if(result == null)
            {
                return false;
            }
            return true;
            
        }

        public bool LogOut()
        {
            return true;
        }
    }
}
