using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootstrapProject.Models
{
    public class Warehouse
    {
        [Key]
        public int WarehouseID { get; set; }    

        public String WarehouseName {get;set;}

        public String Address {get;set;}
    }
}