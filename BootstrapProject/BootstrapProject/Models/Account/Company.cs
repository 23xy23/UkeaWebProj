using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }     
        public string CompanyName { get; set; }     


        public int AccountID {get;set;}
        public Account Account {get;set;}
    }
}