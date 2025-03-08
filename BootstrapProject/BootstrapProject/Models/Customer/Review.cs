using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }    
        public int Rating {get;set;}
        public String ReviewText{get;set;}
        public DateTime ReviewDate  {get;set;} 

        

        public int  CustomerID {get;set;}
        public int ProductID {get;set;}
        public Customer Customer {get;set;}
        public Product Product {get;set;}
    }
}