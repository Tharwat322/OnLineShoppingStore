using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnLineShoppingStore.WebUI.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User Name")]
       public string UserName { get; set; }
        [Required]
        [StringLength(50,MinimumLength =6)]
       public string PassWord { get; set; }
    }
}