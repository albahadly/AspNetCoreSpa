using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Models
{
    public class Customer
    {
        [Display(Name = "First")]
        public string First_name { get; set; }
        [Display(Name = "Last")]
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }


    }
}
