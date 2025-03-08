
using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsID { get; set; }     

        public int Quantity {get;set;}
        public Decimal UnitPrice {get;set;}
        public Decimal TotalPrice {get;set;}


        public int CompanyID {get;set;}
        public int OrderID {get;set;}
        public int ProductWarehouseID {get;set;}

        public Company Company {get;set;}
        public Order Order {get;set;}
        public ProductWarehouse ProductWarehouse {get;set;}


    }
}