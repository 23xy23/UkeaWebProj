using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootstrapProject.Models
{
    public class Task
    {
        [Key]
        public int TaskID { get; set; } 
        public String TaskDescription { get; set; }
        public String TaskType { get; set; }
    }
}