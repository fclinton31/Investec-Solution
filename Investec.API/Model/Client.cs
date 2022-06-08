using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Investec.API.Model
{
    public class Client
    {
       //[Required]
        public string FirstName { get; set; }
        //[Required]
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        //[Required]
        public string IDNumber { get; set; }
        public Address PhysicalAddress { get; set; }
    }
}
