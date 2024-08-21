using _MVC_Identity_.Entities.Concreate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _MVC_Identity_.Controllers
{
    [Authorize(Roles="Admin")]
    public class AppUserController : Controller
    {
        private readonly UserManager<AppUser> _userMenager;
        public AppUserController(UserManager<AppUser> userMenager)
        {
            _userMenager = userMenager; 
        }
        public IActionResult Index()
        {
            return View(_userMenager.Users);
        }
    }
}
