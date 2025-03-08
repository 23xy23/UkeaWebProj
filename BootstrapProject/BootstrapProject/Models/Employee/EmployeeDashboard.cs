using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootstrapProject.Models{
    public class EmployeeDashboard{
        public int totalEmployees {get; set;}
        public decimal grossSalary {get; set;}
        public IEnumerable<EmployeeTitleDboard> allTitles {get; set;}
        public IEnumerable<EmployeeAllDboard> allEmployees {get; set;}
    }

    public class EmployeeTitleDboard{
        public string title {get; set;}
        public int count {get; set;}
    }

    public class EmployeeAllDboard{
        public int employeeId {get; set;}
        public string name {get; set;}
        public string employeeTitle {get; set;}
        public decimal salary {get;set;}
        public decimal cpfContribution {get;set;}
        public decimal netSalary{get;set;}
    }
}