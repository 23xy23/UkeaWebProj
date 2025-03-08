using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }    

        public String CategoryName {get;set;}

        public String Description {get;set;}
    }
}