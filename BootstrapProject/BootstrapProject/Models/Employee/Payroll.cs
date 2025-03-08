using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class Payroll
    {
        [Key]
        public int PayrollID { get; set; }     
        public DateTime DateOfPayment {get;set;}   
        public Decimal Amount {get;set;}   
        public bool isPaid {get;set;}   

        public int EmployeeID { get; set; } 
        public Employee Employee {get;set;}
    }
}