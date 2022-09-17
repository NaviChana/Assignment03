using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assignment03
{
    [Serializable]
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string? ProductName { get; set; }
        public int? CategoryID { get; set; }

        [StringLength(20)]
        public string? QuantityPerUnit { get; set; }
        public short? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

    }
}