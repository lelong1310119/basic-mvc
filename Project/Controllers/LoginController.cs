using Microsoft.AspNetCore.Mvc;
using Project.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Project.Controllers
{
    public class LoginController : Controller
    {
        private readonly ManageContext _context;

        public LoginController(ManageContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") is null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Login");
        }

        //[HttpPost]
        //public IActionResult Index(string userName, string password)
        //{
        //    if (userName == "admin" && password == "123456")
        //    {
        //        HttpContext.Session.SetString("username", userName);
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Tên đăng nhập hoặc mật khẩu không đúng";
        //        return View();
        //    }
        //}
        public string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);
                var sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            password = MD5Hash(password);  
            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.Username == username && a.Password == password);

            if (account != null)
            {
                HttpContext.Session.SetString("username", account.Username);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Tên đăng nhập hoặc mật khẩu không đúng";
                return RedirectToAction("Index", "Login"); ;
            }
        }
    }
}
