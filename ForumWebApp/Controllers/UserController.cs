using Forum.Application.MainTopics;
using Forum.Application.MainUsers;
using Forum.Domain.user;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForumWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITopicService _topicService;
        private readonly SignInManager<User> _signInManager;

        public UserController(IUserService userService, SignInManager<User> signInManager, ITopicService topicService)
        {
            _userService = userService;
            _signInManager = signInManager;
            _topicService = topicService;
        }

       
        public async Task<IActionResult> Ban(int id, [FromQuery] bool isBanned, CancellationToken token = default)
        {
            await _userService.BanUserAsync(id, !isBanned);
            return RedirectToAction("Users", "Admin");
        }

        public async Task<IActionResult> Index2(string email, CancellationToken token = default)
        {
            var userPage = await _userService.GetUserByEmailAsync(token, email);
            return RedirectToAction("Index", new {id = userPage.User!.Id});
        }
        public async Task<IActionResult> Index(int id, CancellationToken token = default)
        {
            var user = await _userService.GetUserByIdAsync(id);
            var userPage = await _userService.GetUserByEmailAsync(token, user.Email);
            return View(userPage);
        }

        public async Task<IActionResult> Edit(CancellationToken token = default)
        {
            var currentUser = HttpContext.User;
            var curUserId = int.Parse(_signInManager.UserManager.GetUserId(currentUser));

            var user = await _userService.GetUserByIdAsync(curUserId);
            var updatedUser = user.Adapt<UserUpdateModel>();

            return View(updatedUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateModel updateModel, CancellationToken token = default)
        {
            var currentUser = HttpContext.User;
            var curUserId = int.Parse(_signInManager.UserManager.GetUserId(currentUser));

            await _userService.UpdateUsercAsync(curUserId, updateModel);

            return RedirectToAction("Index", new {id = curUserId});
        }

        public async Task<IActionResult> Delete(CancellationToken token = default)
        {
            var currentUser = HttpContext.User;
            var curUserId = int.Parse(_signInManager.UserManager.GetUserId(currentUser));

            var user = await _topicService.GetTopicByIdAsync(curUserId, token);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteUser(CancellationToken token = default)
        {
            var currentUser = HttpContext.User;
            var curUserId = int.Parse(_signInManager.UserManager.GetUserId(currentUser));

            await _userService.DeleteUserAsync(curUserId);
            return RedirectToAction("Index", "Home");
        }

    }
}
