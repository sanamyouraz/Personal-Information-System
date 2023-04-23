using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Person_Information_System.Models
{
    public class PersonInfo
    {


        public int PersonId { get; set; }
        public string Salutation { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Age { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        //public string Education { get; set; }

        public string nationality { get; set; }

        //public string AddressId { get; set; }


        public virtual List<Address> AddressList { get; set; }

        public virtual List<Educationdata> Educationlist { get; set; }


    }
    public class Educationdata
    {
        public string chosenEdu { get; set; }
    }
}