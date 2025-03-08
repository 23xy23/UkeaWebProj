using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BootstrapProject.Models;

namespace BootstrapProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    private void SetSessionData(){
        ViewBag.SessionId = HttpContext.Session.GetString("SessionId");
        ViewBag.Username = HttpContext.Session.GetString("Username");;
    }

    public IActionResult Index()
    {
        SetSessionData();
        if (string.IsNullOrEmpty(ViewBag.SessionId))
        {
            return RedirectToAction("Login", "Login");
        }
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Overview()
    {
        SetSessionData();
        if (string.IsNullOrEmpty(ViewBag.SessionId))
        {
            return RedirectToAction("Login", "Login");
        }
        return View();
    }

        public IActionResult HumanResources()
    {
        SetSessionData();
        if (string.IsNullOrEmpty(ViewBag.SessionId))
        {
            return RedirectToAction("Login", "Login");
        }
        return View();
    }

        public IActionResult Accounts()
    {
        SetSessionData();
        if (string.IsNullOrEmpty(ViewBag.SessionId))
        {
            return RedirectToAction("Login", "Login");
        }
        return View();
    }

        public IActionResult Customers()
    {
        SetSessionData();
        if (string.IsNullOrEmpty(ViewBag.SessionId))
        {
            return RedirectToAction("Login", "Login");
        }
        return View();
    }

        public IActionResult Inventory()
    {
        SetSessionData();
        if (string.IsNullOrEmpty(ViewBag.SessionId))
        {
            return RedirectToAction("Login", "Login");
        }
        return View();
    }

        public IActionResult Feedback()
    {
        return View();
    }

        public IActionResult Help()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
