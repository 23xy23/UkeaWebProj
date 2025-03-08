using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class SystemRole
    {
        [Key]
        public int SystemRoleID { get; set; }     
        public String RoleName { get; set; } 
        public List<Employee> Employees {get;set;}   
    }
}