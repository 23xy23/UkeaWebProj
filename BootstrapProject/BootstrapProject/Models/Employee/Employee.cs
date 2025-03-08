using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BootstrapProject.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID {get;set;}
        public string FirstName {get; set;}
        public string LastName { get; set; }
        public Decimal Salary{get;set;}


        public int SystemRoleID {get;set;}
        public int AccountID { get; set; }     
        public int EmployeeTypeID { get; set; }     

        public SystemRole SystemRole {get;set;}
        public Account Account {get;set;}
        public EmployeeType EmployeeType {get;set;}
        public int UserID{get;set;}
        public User User { get; set; }
    }
}