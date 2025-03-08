using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace BootstrapProject.Models
{
    public class AccountType
    {
        [Key]
        public int AccountTypeID { get; set; }     
        public string AccountTypeName { get; set; }     
    
    }
}