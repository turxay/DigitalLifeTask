using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTech.Areas.AdminDigitalLife.Models;
using TaskTech.Areas.AdminDigitalLife.ViewModels;
using TaskTech.Helpers;

namespace TaskTech.Areas.AdminDigitalLife.Controllers
{
    [Area("AdminDigitalLife")]
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM  login )
        {
            if (!ModelState.IsValid) return View(login);
            AppUser loginUser = await _userManager.FindByEmailAsync(login.Email);
            if (loginUser == null)
            {
                ModelState.AddModelError("", "Mail is invalid!!!");
                return View(login);
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult =
                await _signInManager.PasswordSignInAsync(loginUser, login.Password, false, true);

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Password is invalid!!!");
                return View(login);
            }
            string role = (await _userManager.GetRolesAsync(loginUser))[0];
            if (role == Helper.Roles.Admin.ToString())
            {
                return RedirectToAction("Index", "Dashboard", new { area = "AdminDigitallife" });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public async Task CreateRole()
        {
            if (!await _roleManager.RoleExistsAsync(Helper.Roles.Admin.ToString()))
                await _roleManager.CreateAsync(new IdentityRole(Helper.Roles.Admin.ToString()));

            if (!(await _roleManager.RoleExistsAsync(Helper.Roles.Member.ToString())))
                await _roleManager.CreateAsync(new IdentityRole(Helper.Roles.Member.ToString()));
        }

        public async Task<IActionResult> createusr()
        {
            List<AppUser> users = _userManager.Users.ToList();
            List<UserVM> usersVM = new List<UserVM>();
            foreach (AppUser user in users)
            {
                UserVM userVM = new UserVM
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Fullname = user.Fullname,
                    Role = await _userManager.GetRolesAsync(user)
                };

                usersVM.Add(userVM);
            }

            return Json(usersVM);
        }
    }
}
