using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop5_CPRG214_WebApp.Models
{
    public partial class Packages_Products_Suppliers
    {
        [Key]
        [ForeignKey("PackageId")]
        public int PackageId { get; set; }

        [ForeignKey("ProductSupplierId")]
        public int? ProductSupplierId { get; set; }

        public Packages Package { get; set; }
        //public ProductsSupplier ProductSupplier { get; set; }
    }
}
