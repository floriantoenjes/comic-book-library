using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ComicBookLibraryManagerWebApp.ViewModels;
using ComicBookShared.Data;
using ComicBookShared.Models;
using ComicBookShared.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;

namespace ComicBookLibraryManagerWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly BaseUserRepository _userRepository;
        private readonly BaseComicBooksRepository _comicBooksRepository;

        public AccountController(
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            IAuthenticationManager authenticationManager,
            BaseUserRepository userRepository,
            BaseComicBooksRepository comicBooksRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
            _userRepository = userRepository;
            _comicBooksRepository = comicBooksRepository;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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

        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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
                    return Redirect(Request.QueryString["ReturnUrl"]);
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

        public ActionResult Favorites()
        {
            var favoriteComicBooks = _userRepository.GetFavoriteComicBooks(User.Identity.GetUserId());
            return View(favoriteComicBooks);
        }

        [HttpPost]
        public JsonResult RemoveFavorite(int id)
        {
            var comicBook = _comicBooksRepository.Get(id);

            if (comicBook == null)
            {
                return Json(false);
            }

            var user = _userManager.FindByName(User.Identity.Name);
            comicBook.UsersWhoChoseAsFavorite.Remove(user);
            _comicBooksRepository.Update(comicBook);

            return Json(true);
        }

        [HttpPost]
        public JsonResult AddFavorite(int id)
        {
            var comicBook = _comicBooksRepository.Get(id);

            if (comicBook == null)
            {
                return Json(false);
            }

            var user = _userManager.FindByName(User.Identity.Name);
            comicBook.UsersWhoChoseAsFavorite.Add(user);
            _comicBooksRepository.Update(comicBook);

            return Json(true);
        }

    }
}