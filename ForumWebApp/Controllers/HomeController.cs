using Microsoft.AspNetCore.Mvc;
using Forum.Application.MainTopics;
using Microsoft.AspNetCore.Identity;
using Forum.Domain.user;
using Mapster;

namespace ForumWebApp.Controllers
{

    public class HomeController : Controller
    {
        private readonly ITopicService _topicService;

        public HomeController(ITopicService topicService)
        {
            _topicService = topicService;
        }
        public async Task<IActionResult> Index(CancellationToken token = default)
        {
            var topics = await _topicService.GetShowingTopicsAsync(token);
            return View(topics);
        }

    }
}
