using _MVC_Identity_.Entities.Concreate;
using _MVC_Identity_.Models.ViewModel;
using _MVC_Identity_.Models.ViewModel.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace _MVC_Identity_.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                appUser.CreatedBy = "Bengusu";
                IdentityResult identityResult = await _userManager.CreateAsync(appUser, model.Password);

                if (identityResult.Succeeded)
                {

                    return RedirectToAction("Login");
                }

                else
                {
                    foreach (IdentityError error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }



            }
            return View(model);
        }

        public IActionResult Login(string returnUrl)
        {
            returnUrl = returnUrl is null ? "Index" : returnUrl;
            return View(new LoginVM{ ReturnURL= returnUrl });
        }
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByNameAsync(model.UserName);
                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser.UserName, model.Password, false, false);
                    if (result.Succeeded)

                        return Redirect(model.ReturnURL != null ? model.ReturnURL: Url.Action("Index", "Home"));
                    ModelState.AddModelError("", "RONG CREDATİON İFORMATİON.");

                }
            }
            else
            {
                ModelState.AddModelError("Kullanıcı Bulunamadı", "Girdiğiniz bilgilerle kullanıcı bulunmadı,Lütfen tekrara deneyinz.");
            }
            return View(model);
        }

        public async Task< IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View();
        }
        public async Task< IActionResult >Edit()
        {
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateDTO userUpdateDTO = new UserUpdateDTO()
            {
                Email = appUser.Email,
                Password = appUser.PasswordHash,
                UserName = appUser.UserName,

            };
            return View(userUpdateDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                appUser.UserName = model.UserName;
                if (model.Password != null)
                {
                    appUser.PasswordHash = _passwordHasher.HashPassword(appUser, model.Password);
                }
                IdentityResult result = await _userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Your profile has been edited!";
                }
                else
                    TempData["Error"] = "Your Profile has not been edited!";
            }
            return View(model);
        }
    }
}