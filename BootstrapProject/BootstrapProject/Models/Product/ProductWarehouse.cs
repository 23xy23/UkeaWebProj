using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootstrapProject.Models
{
    public class ProductWarehouse
    {
        [Key]
        public int ProductWarehouseID { get; set; }    
        public int Quantity {get;set;}
        public Decimal UnitPrice {get;set;}


        public int ProductID {get;set;}
        public int WarehouseID {get;set;}
        public Product Product {get;set;}
        public Warehouse Warehouse {get;set;}

    }
}