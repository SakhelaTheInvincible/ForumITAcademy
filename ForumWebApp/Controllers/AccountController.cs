using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Forum.Application.MainUsers;
using Forum.Persistence.Identity;
using Forum.Domain.user;

namespace ForumWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        public IActionResult Login()
        {
            var response = new UserLoginModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid) return View(userLoginModel);
            var user = await _userService.AuthenticationAsync(userLoginModel);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            var response = new UserCreateModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserCreateModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);
            var user = await _userManager.FindByNameAsync(registerViewModel.UserName);
            if (user != null)
            {
                TempData["Error"] = "This username already exists";
                return View(registerViewModel);
            }

            await _userService.CreateUserAsync(registerViewModel);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
