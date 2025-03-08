using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BootstrapProject.Models;
using BootstrapProject.Data;
using BootstrapProject.ViewModels;
using System.Text.Json;



namespace UKEA.Controllers
{
    public class CustomerController : Controller
    {

        // Initialize Database
        public ApplicationDbContext Database
        {
            get;
        }

        private readonly ApplicationDbContext _db;

        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: /<controller>/
        public IActionResult Index()
        
        {
            // Define the current month range
            var currentMonthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var nextMonthStart = currentMonthStart.AddMonths(1);

            // Define the previous month range
            var previousMonthStart = currentMonthStart.AddMonths(-1);
            var previousMonthEnd = currentMonthStart;

            // Fetch the total customers for the current month
            var currentMonthUsers = _db.Customers
                .Where(c => c.User.CreatedAt >= currentMonthStart && c.User.CreatedAt < nextMonthStart)
                .ToList();
            var currentMonthCount = currentMonthUsers.Count;

            // Fetch the total customers for the previous month
            var previousMonthUsers = _db.Customers
                .Where(c => c.User.CreatedAt >= previousMonthStart && c.User.CreatedAt < previousMonthEnd)
                .ToList();
            var previousMonthCount = previousMonthUsers.Count;

            // Calculate the percentage increase or decrease
            double customerPercentageChange = 0;
            if (previousMonthCount > 0)
            {
                customerPercentageChange = ((double)(currentMonthCount - previousMonthCount) / previousMonthCount) * 100;
            }

            // Fetch the total Orders for the current month
            var currentMonthOrders = _db.Orders
                .Where(o => o.OrderDate >= currentMonthStart && o.OrderDate < nextMonthStart)
                .ToList();
            var currentOrdersCount = currentMonthOrders.Count;

            // Fetch the total Orders for the current month
            var previousMonthOrders = _db.Orders
                .Where(o => o.OrderDate >= previousMonthStart && o.OrderDate < previousMonthEnd)
                .ToList();
            var previousOrdersCount = previousMonthOrders.Count;

            // Calculate the percentage increase or decrease for orders
            double orderPercentageChange = 0;
            if (previousMonthCount > 0)
            {
                orderPercentageChange = ((double)(currentOrdersCount - previousOrdersCount) / previousOrdersCount) * 100;
            }

            // Fetch the total interaction for the current month
            var currentMonthInteractions = _db.Interactions
                .Where(i => i.InteractionDate >= currentMonthStart && i.InteractionDate < nextMonthStart)
                .ToList();
            var currentInteractionsCount = currentMonthInteractions.Count;

            // Fetch the total interactions for the current month
            var previousMonthInteractions = _db.Interactions
                .Where(i => i.InteractionDate >= previousMonthStart && i.InteractionDate < previousMonthEnd)
                .ToList();
            var previousInteractionsCount = previousMonthInteractions.Count;

            // Calculate the percentage increase or decrease for interactions
            double interactionPercentageChange = 0;
            if (previousMonthCount > 0)
            {
                interactionPercentageChange = ((double)(currentInteractionsCount - previousInteractionsCount) / previousInteractionsCount) * 100;
            }

            //Retrieve Urgent Tasks
            var today = DateTime.Today;
            var urgentTasks = _db.CustomerTasks
                .Where(c => c.Status != "completed" && (c.DueDate <= today.AddDays(5) ||c.DueDate < today))
                .Select(c => new CustomerDashboardVM.UrgentTaskVM
                {
                    CustomerTaskID = c.CustomerTaskID, // Populate the CustomerTaskID property
                    TaskDescription = c.Task.TaskDescription,
                    TaskType = c.Task.TaskType,
                    TaskDueDate = c.DueDate
                })
                .ToList();

            var totalTaskCount = _db.CustomerTasks.Count();
            var pendingTasksCount = _db.CustomerTasks.Where(c => c.Status != "completed").ToList().Count;
            double tasksCompletionPercentage = 100 - (((double)pendingTasksCount / totalTaskCount)  * 100);
            var customerDashboardVM = new CustomerDashboardVM
                {
                    Customers = _db.Customers.Include(c => c.User).ToList(),
                    Orders = _db.Orders.ToList(),
                    Interactions = _db.Interactions.ToList(),
                    Reviews = _db.Reviews.ToList(),
                    CustomerTasks = _db.CustomerTasks.Include(t => t.Task).ToList(),
            //         Tasks = _db.Tasks.ToList(),
                    UserCountToday = GetUserCountCreatedToday(),
                    OrdersTotal = GetOrdersCount(),
                    InteractionsTotal = GetInteractionsCount(),
                    CustomerTotalMonth = previousMonthCount,

                    // CustomerTotalMonth = currentMonthCount,
                    InteractionPercentageChange = interactionPercentageChange,
                    CustomerPercentage = customerPercentageChange,
                    OrdersPercentage = orderPercentageChange,
                    UrgentTasks = urgentTasks,
                    TasksCompletionPercentage = tasksCompletionPercentage,
                };

            ViewBag.TasksCompletionPercentage = tasksCompletionPercentage;
            ViewBag.TotalCustomers = currentMonthCount;
            ViewBag.CustomerPercentageChange = customerPercentageChange;
            ViewBag.UserCountToday = GetUserCountCreatedToday();
            ViewBag.OrdersTotal = GetOrdersCount();
            ViewBag.InteractionsTotal = GetInteractionsCount();
            ViewBag.OrdersPercentage = orderPercentageChange;
            ViewBag.InteractionsPercentage = interactionPercentageChange;


            return View(customerDashboardVM);
        }



        private int GetUserCountCreatedToday()
        {
            return _db.Customers.Count();
            // var today = DateTime.Today;
            // return _db.Users.Count(u => u.CreatedAt.Date == today);
        }


        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer model)
        {
            var customer = new Customer
            {
                DateOfBirth = model.DateOfBirth,
                CreditCardInfo = model.CreditCardInfo,
                UserID = model.UserID
            };

            _db.Customers.Add(customer);
            _db.SaveChanges();

            // Process the customer data
            // Save to database or perform other operations
            return Ok();
}



        [HttpDelete]
        public IActionResult DeleteCustomer(int id)
        {
            var obj = _db.Customers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Customers.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Customer deleted successfully";

            return Ok(); 
        }


        private int GetOrdersCount()
        {
            return _db.Orders.Count();
        }

        public IActionResult OrderChart()
        {
            var data = _db.Orders
                        .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                        .Select(g => new {
                            Year = g.Key.Year,
                            Month = g.Key.Month,
                            TotalOrders = g.Count()
                        })
                        .OrderBy(g => g.Year)
                        .ThenBy(g => g.Month)
                        .ToList();

            var jsonData = JsonSerializer.Serialize(data);
            return Json(data);
        }

        private int GetInteractionsCount()
        {
            return _db.Interactions.Count();
        }

            // POST: /CustomerTask/Update
        [HttpPost]
        [Route("Customer/UpdateCustomerTaskStatus")]
        public IActionResult UpdateCustomerTaskStatus(int taskId, string taskDescription, bool isChecked)
        {
            var task = _db.CustomerTasks.FirstOrDefault(c => c.CustomerTaskID == taskId && c.Task.TaskDescription == taskDescription);
            if (task != null)
            {
                task.Status = isChecked ? "Completed" : "Pending";
                _db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
        //Add Customer Tasks button
        public IActionResult AddCustomerTask()
        {
            return PartialView("AddCustomerTask");
        }

        [HttpPost]
        public IActionResult CreateCustomerTask(int customerId, int taskId, int assignEmployeeId, DateTime dueDate)
        {
            var task = new CustomerTask
            {
                Status = "Pending",
                DueDate = dueDate,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CustomerID = customerId,
                TaskID = taskId,
                AssignEmployeeID = assignEmployeeId
            };

            _db.CustomerTasks.Add(task);
            _db.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult DeleteReview(int reviewId)
        {
            // Retrieve the review from the database using the reviewId
            var review = _db.Reviews.FirstOrDefault(r => r.ReviewID == reviewId);

            if (review == null)
            {
                return NotFound(); // Or handle the case where the review is not found
            }

            // Remove the review from the database
            _db.Reviews.Remove(review);

            // Save changes to the database
            _db.SaveChanges();

            return Ok(); // Return a success response
        }

    }
}