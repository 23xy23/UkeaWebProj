using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class CustomerTask
    {
        [Key]
        public int CustomerTaskID { get; set; }     
        public String Status { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public int CustomerID { get; set; }    
        public int TaskID {get;set;}
        public int AssignEmployeeID{get;set;}
        public Customer Customer {get;set;} 
        public Task Task {get;set;}
        public Employee Employee{get;set;}

    }
}