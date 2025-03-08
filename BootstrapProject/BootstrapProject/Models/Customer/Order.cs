using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }    
        public DateTime OrderDate  {get;set;} 
        public Decimal DeliveryPrice {get;set;}
        public String PaymentType {get;set;}  
        public String ShippingAddress{get;set;}  

        
        public int  CustomerID {get;set;}
        public Customer Customer {get;set;}
    }
}