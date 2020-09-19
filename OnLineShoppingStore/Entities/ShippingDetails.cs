using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineShoppingStore.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage ="Please enter A name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please enter the first Address  line")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage ="Please Enter City Name")]
        public string City { get; set; }

        [Required(ErrorMessage ="Please enter State Name")]
        public string State { get; set; }
        public string Zip{ get; set; }

        [Required(ErrorMessage ="Please enter country Name")]
        public string Country{ get; set; }
        public bool GiftWrap { get; set; }

    }
}
