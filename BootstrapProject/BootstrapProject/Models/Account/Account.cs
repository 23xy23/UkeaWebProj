using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }     
        public string AccountName { get; set; }     
        public string isCompanyOrEmployee { get; set; }     

        public Decimal Balance {get;set;}
        public DateTime UpdatedAt {get;set;}
        public String Currency {get;set;}
        public String Description {get;set;}

        public int AccountTypeID {get;set;}
        public int EmployeeID {get;set;}
        public int CompanyID {get;set;}

        public AccountType AccountType{get;set;}

        // Navigation properties
        public Employee Employee { get; set; }
        public Company Company { get; set; }


    }
}