﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Product
    {
        public long ProductId { get; set; }

        [Required(ErrorMessage ="Please enter a product name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please enter a description")]
        public string Description { get; set; }

        [Column(TypeName ="decimal(8,2)"), Required(ErrorMessage ="A decimal price is required"), 
            Range(0.01,double.MaxValue, ErrorMessage ="Please enter a positive integer")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage ="Please specify a category")]
        public string Category { get; set; }
    }
}
