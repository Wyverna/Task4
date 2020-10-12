using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task4.Models;

namespace Task4.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            try {
                if (User.Identity.IsAuthenticated)
                    if ((await _userManager.FindByNameAsync(User.Identity.Name)).Status != "Blocked")
                        return View(_userManager.Users.ToList());
                return RedirectToAction("Login", "Account"); }
            catch (Exception e) {
                return RedirectToAction("Login", "Account");}
        }
        [HttpPost]
        public async Task<IActionResult> Method(String submit2,String[] id)
        {
            TempData["id"] = id;
            if (User.Identity.IsAuthenticated)
                if ((await _userManager.FindByNameAsync(User.Identity.Name)).Status != "Blocked")
                    return RedirectToAction(submit2, "Home");
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Block()
        {
            foreach (String i in (String[])TempData["id"])
            {
                User user = await _userManager.FindByIdAsync(i);
                user.Status = "Blocked";
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index", "Home"); ;
        }

        public async Task<IActionResult> Unblock()
        {
            foreach (String i in (String[])TempData["id"])
            {
                User user = await _userManager.FindByIdAsync(i);
                user.Status = "Unblocked";
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index", "Home"); ;
        }

        public async Task<IActionResult> Delete()
        {
            foreach (String i in (String[])TempData["id"])
            {
                User user = await _userManager.FindByIdAsync(i);
                if (User.Identity.Name == user.UserName)
                    await _signInManager.SignOutAsync();
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index", "Home"); ;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
