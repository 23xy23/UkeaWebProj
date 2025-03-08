using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BootstrapProject.Data;
using BootstrapProject.Models;

namespace BootstrapProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly byte[] HardcodedSalt;

        public LoginController(ILogger<LoginController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            HardcodedSalt=HashSHA512("salt123");

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Model state is valid.");
                
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
                if (user != null)
                {
                    _logger.LogInformation($"User found: {user.Username}");
                    
                    byte[] hashedPassword = HashPassword(model.Password);
                    if (VerifyPassword(hashedPassword, user.PasswordHash))
                    {
                        _logger.LogInformation("Password verification succeeded.");
                        
                        user.LastLogin = DateTime.UtcNow;
                        await _context.SaveChangesAsync();

                        //generate session ID
                        string sessionId = Guid.NewGuid().ToString();
                        HttpContext.Session.SetString("SessionId", sessionId);
                        HttpContext.Session.SetString("Username", user.Username);


                        TempData["Success"]="Successfully logged in ";
                        TempData["Username"] = user.Username;

                        return RedirectToAction("Index","Inventory");
                    
                    }
                    else
                    {
                        TempData["Error"]= "Invalid username or password.";
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    }
                }
                else
                {
                    _logger.LogWarning("User not found.");
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
            }
            else
            {
                _logger.LogWarning("Model state is invalid.");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("Login","Login");
        }

        private bool VerifyPassword(byte[] computedHash, byte[] storedHash)
        {
            bool isMatch = computedHash.SequenceEqual(storedHash);
            if (isMatch)
            {
                _logger.LogInformation("Computed hash matches stored hash.");
            }
            else
            {
                _logger.LogWarning("Computed hash does not match stored hash.");
            }
            return isMatch;
        }

        private byte[] HashPassword(string password)
        {
            return HashSHA512(password);
        }

        private byte[] HashSHA512(string input)
        {
            using (var sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
                return hashBytes;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
