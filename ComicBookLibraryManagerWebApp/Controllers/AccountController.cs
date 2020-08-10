using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ComicBookLibraryManagerWebApp.ViewModels;
using ComicBookShared.Security;
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

            return View(viewModel);
        }
    }
}