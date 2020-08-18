using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.Models
{
    public class Product
    {
        public long ProductId { get; set; }

        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        [Required(ErrorMessage ="Insert a price")]
        [Range(0.01,9_999_999)]
        //[DisplayFormat(DataFormatString ="{0:c2}", ApplyFormatInEditMode =true)]
        public decimal Price { get; set; }

        
        public long CategoryId { get; set; }

        public Category Category { get; set; }

        
        public long SupplierId { get; set; }

        public Supplier Supplier { get; set; }
    }
}
