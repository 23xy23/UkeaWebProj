using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootstrapProject.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }    
        public String SupplierName {get;set;}
        public int ContactNo{get;set;}
        public String Address {get;set;}
    }
}