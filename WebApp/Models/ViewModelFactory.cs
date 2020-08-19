﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ViewModelFactory
    {
        public static ProductViewModel Details(Product p)
        {
            return new ProductViewModel()
            {
                Product = p,
                Action = "Details",
                ReadOnly = true,
                Theme = "info",
                ShowAction = false,
                Categories = p == null ? Enumerable.Empty<Category>() : new List<Category> { p.Category },
                Suppliers = p == null ? Enumerable.Empty<Supplier>() : new List<Supplier> { p.Supplier }
            };
        }

        public static ProductViewModel Create(Product p, IEnumerable<Category> categories, IEnumerable<Supplier> suppliers)
        {
            return new ProductViewModel()
            {
                Product = p,
                Categories = categories,
                Suppliers = suppliers
            };
        }

        public static ProductViewModel Edit(Product p, IEnumerable<Category> categories, IEnumerable<Supplier> suppliers)
        {
            return new ProductViewModel()
            {
                Product = p,
                Action = nameof(Edit),
                ShowAction = true,
                ReadOnly = false,
                Theme = "warning",
                Categories = categories,
                Suppliers = suppliers
            };
        }

        public static ProductViewModel Delete(Product p, IEnumerable<Category> categories, IEnumerable<Supplier> suppliers)
        {
            return new ProductViewModel()
            {
                 Action = "Delete",
                 Product = p,
                 Categories = categories,
                 Suppliers = suppliers,
                 ShowAction = true,
                 Theme = "danger"
            };
        }
    }
}
