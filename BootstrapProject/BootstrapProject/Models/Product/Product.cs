using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootstrapProject.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }    

        [Required(ErrorMessage = "Product Name is required.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "SKU is required.")]
        public string SKU { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        public string imageUrl { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Unit Price must be a non-negative number.")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Cost Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Cost Price must be a non-negative number.")]
        public decimal CostPrice { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateReceived { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateSold { get; set; }

        [Required(ErrorMessage = "Condition is required.")]
        public string Condition { get; set; }

        [Required(ErrorMessage = "Minimum Stock Level is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Minimum Stock Level must be a non-negative number.")]
        public int MinStockLevel { get; set; }

        [Required(ErrorMessage = "Reorder Level is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Reorder Level must be a non-negative number.")]
        public int ReorderLevel { get; set; }

        [Required(ErrorMessage = "Lead Time is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Lead Time must be a non-negative number.")]
        public int LeadTime { get; set; }

        public string BatchNo { get; set; }

        public string Notes { get; set; }

        [Required(ErrorMessage = "Brand ID is required.")]
        public int BrandID { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Supplier ID is required.")]
        public int SupplierID { get; set; }

        public Brand? Brand { get; set; }
        public Category? Category { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
