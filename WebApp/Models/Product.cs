using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models
{
    public class Product
    {
        public long ProductId { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        //[DisplayFormat(DataFormatString ="{0:c2}", ApplyFormatInEditMode =true)]
        public decimal Price { get; set; }

        [Remote("CategoryKey","Validation",ErrorMessage ="Enter an existing category Id")]
        public long CategoryId { get; set; }

        public Category Category { get; set; }

        [Remote("SupplierKey","Validation", ErrorMessage ="Enter an existing supplier Id")]
        public long SupplierId { get; set; }

        public Supplier Supplier { get; set; }
    }
}
