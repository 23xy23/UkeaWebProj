using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class EmployeeType
    {
        [Key]
        public int EmployeeTypeID { get; set; }     
        public String EmployeeTitle { get; set; } 
        public Decimal BaseSalary {get;set;}   
    }
}