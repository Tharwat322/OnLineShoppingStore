using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineShoppingStore.Abstract
{
  public  interface IAuthentication
    {
        bool Authenticate(string username, string password);
        bool LogOut();
    }
}
