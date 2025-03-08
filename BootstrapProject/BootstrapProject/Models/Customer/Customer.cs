using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootstrapProject.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID {get;set;}
        public DateTime DateOfBirth { get; set; }
        public String CreditCardInfo  {get;set;}

        public int UserID{get;set;}
        public User User { get; set; }
    }
}