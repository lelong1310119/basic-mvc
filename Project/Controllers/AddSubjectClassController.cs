using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    public class AddSubjectClassController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") is null)
            {
                return RedirectToAction("Index", "Login");
                // session không tồn tại, thực hiện các xử lý cần thiết ở đây
            }
            else
            {
                return View();
                // session có tồn tại, thực hiện các xử lý cần thiết ở đây
            }
        }
    }
}
