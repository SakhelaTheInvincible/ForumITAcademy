using Forum.Application.MainTopics;
using Forum.Application.MainUsers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ForumWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITopicService _topicService;

        public AdminController(IUserService userService, ITopicService topicService)
        {
            _userService = userService;
            _topicService = topicService;
        }

        public async Task<IActionResult> Topics(CancellationToken token = default)
        {
            var result = await _topicService.GetAllTopicsAsync(token);
            return View(result);
        }

        public async Task<IActionResult> Users(CancellationToken token = default)
        {
            var result = await _userService.GetAllUserAsync(token);
            return View(result);
        }
    }
}
