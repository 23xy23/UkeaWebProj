using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class Interaction
    {
        [Key]
        public int InteractionID { get; set; }     
        public DateTime InteractionDate { get; set; }
        public String Subject { get; set; }
        public String Details { get; set; }
        public String Outcome { get; set; }



        public int CustomerID { get; set; }    
        public int EmployeeID { get; set; }    

        public Customer Customer {get;set;} 
        public Employee Employee{get;set;}

    }
}