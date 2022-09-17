using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assignment03
{
    [Serializable]
    public class Customer
    {
        public string? CustomerID { get; set; }
        public string? City { get; set; }
        public string? CompanyName { get; set; }
    }
}