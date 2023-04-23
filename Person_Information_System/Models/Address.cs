using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Person_Information_System.Models
{
    public class Address
    {
  
        //public int AddressId { get; set; }

        public string AddressType { get; set; }

        public string Province { get; set; }
       
        public string City { get; set; }
    
        public string selectedCat { get; set; }
     
        public string Ward { get; set; }
      
        public string Tole { get; set; }
       

    }
}