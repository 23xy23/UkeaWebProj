using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class Absence
    {
        [Key]
        public int AbsenceID { get; set; }     
        public DateTime DateOfAbsence {get;set;}
        public String ReasonOfAbsence {get;set;}
        public bool isApproved {get;set;}     


        public int EmployeeID { get; set; } 
        public Employee Employee {get;set;}
    }
}