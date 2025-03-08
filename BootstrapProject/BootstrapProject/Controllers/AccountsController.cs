using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BootstrapProject.Models;
using BootstrapProject.Data;

namespace UKEA.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AccountsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var viewModel = new AccountsViewModel
            {
                AccountEntries = _db.AccountEntries.ToList(),
                CashFlow = _db.CashFlow.ToList()
            };

            return View(viewModel);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }
    }
}
