using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class Brand
    {
        [Key]
        public int BrandID { get; set; }   

        public String BrandName {get;set;}

        public String Description {get;set;}

    }
}