using System;
using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class AccountEntries
    {
        [Key]
        public int AccountID { get; set; }     

        [Required]
        [StringLength(255)]
        public string AccountName { get; set; }     

        [Required]
        [StringLength(50)]
        public string AccountType { get; set; }

        public decimal Balance { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateUpdated { get; set; }

        [StringLength(3)]
        public string Currency { get; set; }

        public int EmployeeID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}
