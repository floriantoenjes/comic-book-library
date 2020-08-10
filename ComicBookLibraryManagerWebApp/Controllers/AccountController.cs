using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ComicBookLibraryManagerWebApp.ViewModels;
using ComicBookShared.Models;
using ComicBookShared.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace ComicBookLibraryManagerWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly IAuthenticationManager _authenticationManager;

        public AccountController(
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(AccountRegisterViewModel viewModel)
        {
            var existingUser = await _userManager.FindByEmailAsync(viewModel.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", $"The provided email address '{viewModel.Email}' has already been used to register an account. Please sign-in using your existing account.");
            }

            if (ModelState.IsValid)
            {
                var user = new User() { UserName = viewModel.Email, Email = viewModel.Email };

                var result = await _userManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index", "ComicBooks");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }

            }

            return View(viewModel);
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(AccountSignInViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "ComicBooks");
                case SignInStatus.Failure:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(viewModel);
                case SignInStatus.LockedOut:
                case SignInStatus.RequiresVerification:
                    throw new NotImplementedException("Identity feature not implemented.");
                default:
                    throw new Exception();
            }
        }

        [HttpPost]
        public ActionResult SignOut()
        {
            _authenticationManager.SignOut();
            return RedirectToAction("SignIn", "Account");
        }

    }
}