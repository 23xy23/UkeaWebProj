using System;
using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class CashFlow
    {
        [Key]
        public int Month { get; set; }     

        [Required]
        [DataType(DataType.Currency)]
        public decimal MonthlyRevenue { get; set; }     

        [Required]
        [DataType(DataType.Currency)]
        public decimal MonthlyExpense { get; set; }  

        [Required]
        [DataType(DataType.Currency)]
        public decimal MonthlyProfit { get; set; }  

        [Required]
        [DataType(DataType.Currency)]
        public decimal MonthlyTarget { get; set; }  
    }
}
