using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BootstrapProject.Models;
using BootstrapProject.Data;


namespace UKEA.Controllers
{
    public class HumanResourceController : Controller
    {
        // Initialize Database
        public ApplicationDbContext Database
        {
            get;
        }

        private readonly ApplicationDbContext _db;

        public HumanResourceController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var employeesQry = (from e in _db.Employees
                            join t in _db.EmployeeTypes on e.EmployeeTypeID equals t.EmployeeTypeID
                            select new {
                                employeeId = e.EmployeeID, 
                                name = e.FirstName+ " "+ e.LastName,
                                employeeTitle = t.EmployeeTitle, 
                                salary = e.Salary
                            });

            EmployeeDashboard viewDashboard = new EmployeeDashboard();
            viewDashboard.totalEmployees = employeesQry.Count();
            viewDashboard.grossSalary = employeesQry.Sum(s=> s.salary);
            var allTitle = employeesQry.GroupBy(g=> g.employeeTitle).Select(s=> new{ title = s.Key, count = s.Count()}).ToList();
            List<EmployeeTitleDboard> titleList = new List<EmployeeTitleDboard>();
            var allEmp = employeesQry.ToList();
            List<EmployeeAllDboard> EmpList = new List<EmployeeAllDboard>();

            foreach (var x in allTitle){
                titleList.Add(new EmployeeTitleDboard(){
                    title = x.title,
                    count = x.count
                });
            }
            viewDashboard.allTitles = titleList.AsEnumerable();

            
            foreach (var x in allEmp){
                EmpList.Add(new EmployeeAllDboard(){
                    employeeId = x.employeeId,
                    name = x.name,
                    employeeTitle = x.employeeTitle,
                    salary = x.salary,
                    cpfContribution = ((x.salary/100)*20),
                    netSalary = ((x.salary/100)*80)
                });
            }
            viewDashboard.allEmployees = EmpList.AsEnumerable();

            return View(viewDashboard);
        }

        public IActionResult NewEmployee()
        {
            return View();
        }

        public IActionResult EditEmployee()
        {
            return View("EditEmployee");
        }
    }
}