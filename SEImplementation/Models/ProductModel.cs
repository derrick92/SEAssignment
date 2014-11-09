using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;


namespace SEImplementation.Models
{
    public class ProductModel
    {
        public int productID { get; set; }
        public string userName { get; set; }

        [Required]
        [Display(Name = "Product name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Product Description")]
        public string ProductDesc { get; set; }

        [Required]
        [Display(Name = "Stock")]
        public int ProductStock { get; set; }

        [Required]
        [Display(Name = "Product Image")]
        public string ProductImage { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal ProductPrice { get; set; }

        [Required]
        [Display(Name = "Seller")]
        public int CreatedBy { get; set; }

        [Required]
        [Display(Name = "Added on")]
        public DateTime DateAdded { get; set; }
    }
}