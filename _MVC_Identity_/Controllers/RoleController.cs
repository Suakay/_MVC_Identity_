using _20_Identity.Models.DTOs;
using _MVC_Identity_.Entities.Concreate;
using _20_Identity.Models.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _20_Identity.Controllers
{
    //Role sayfasına girebilmek için ilk etapta Authorize ihtiyacımız var.
    [Authorize(Roles="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public RoleController(RoleManager<IdentityRole<Guid>> roleManager,UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
         var roles= _roleManager.Roles.Select(x => new { x.Id, x.Name }).ToList();
           List<RoleVM> model = new List<RoleVM>();
            foreach(var item in roles)
            {
                model.Add(new RoleVM() { Id = item.Id, Name = item.Name });
            }


            return View();
        }
        public IActionResult Create() => View();

        [HttpPost]
        public async Task< IActionResult> Create(CreateRoleVM model)
        {
            if (ModelState.IsValid)
            {
             IdentityRole<Guid> role=  await  _roleManager.FindByNameAsync(model.Name);//Elimizdeki name bu roloe veri tabaı ierisine oluşup oluşmadığını kontrol ediyoruz.
                if(role == null)
                {
                  IdentityResult result=await  _roleManager.CreateAsync(new IdentityRole<Guid>() { Name = model.Name });
                    if (result.Succeeded)
                    {
                        TempData["Succeede"] = "The role has been created!";
                        return RedirectToAction("Index");//Role list sayfasına git.
                    }
                    else//Hataları modelstate yüklüyoruz.
                    {
                        foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError("",error.Description);
                            TempData["Eror"]=error.Description;
                        }
                    }
                }
                else//Veri tabanında bu isimde rol varsa Tepdata ile mesaj gönderiyoruz.
                {
                    TempData["Message"] = "BU ĞİSİMDE BİR ROL MEVCUT";

                }

            }
            return View(model);//Validation doğrulanmazsa aynı sayfaya modeli geri gönderiyoruz.
        }

        public async Task<IActionResult> AssignedUser(string id)
        {
            IdentityRole<Guid> identityRole=await _roleManager.FindByNameAsync(id);
            List<AppUser>hasRole=new List<AppUser>();
            List<AppUser>hasNotRole=new List<AppUser>();
            foreach(AppUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, identityRole.Name) ? hasRole : hasNotRole;
                list.Add(user);

            }
            AssingnedRoleDTO assignedRoleDTO = new AssingnedRoleDTO()
            {
                Role=identityRole,
                HasRole=hasRole,
                HasNotRole=hasNotRole,
                RoleName=identityRole.Name



            };
            return View(assignedRoleDTO);

        }
        [HttpPost]
        public async Task<IActionResult>AssignedUser(AssingnedRoleDTO model)
        {
            foreach(var userID in model.AddIds ?? new string[] { }) //model.AddIds nullgelirse diye ?? string[] {}" EKLİYORUZ
            {
                AppUser user =await _userManager.FindByNameAsync(userID);
                IdentityResult result = await _userManager.AddToRoleAsync(user, model.RoleName);
            }
            foreach(var userID in model.DeleteIds ?? new string[] { })
            {
                AppUser user =await _userManager.FindByEmailAsync(userID);
                await _userManager.AddToRoleAsync(user, model.RoleName);
            }
            return RedirectToAction("Index");
        }

    }
}
