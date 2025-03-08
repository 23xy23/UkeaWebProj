using System.Collections.Generic;
using BootstrapProject.Models;
using Microsoft.Identity.Client;

namespace BootstrapProject.ViewModels
{
    public class CustomerDashboardVM
    {
        public IEnumerable<Customer>? Customers { get; set; }
        public IEnumerable<Order>? Orders { get; set; }
        public IEnumerable<Interaction>? Interactions { get; set; }
        public IEnumerable<Review>? Reviews { get; set; }
        public IEnumerable<CustomerTask>? CustomerTasks { get; set; }
        public List<UrgentTaskVM>? UrgentTasks { get; set; } 

        // public IEnumerable<TaskDesc> TaskDescs { get; set; }
        public int UserCountToday { get; set; }
        public int CustomerTotalMonth { get; set; }
        public double CustomerPercentage { get; set; }

        public int OrdersTotal { get; set; }
        public double OrdersPercentage { get; set;}
        public int InteractionsTotal { get; set; }
        public double InteractionPercentageChange { get; set; }
        public class UrgentTaskVM
        {
            public int CustomerTaskID { get; set; } 
            public string TaskDescription { get; set; }
            public string TaskType { get; set; }
            public DateTime TaskDueDate { get; set; }
        }
        public double TasksCompletionPercentage { get; set; }

    }
}