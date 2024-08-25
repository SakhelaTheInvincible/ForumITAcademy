using Forum.Application.MainTopics;
using Forum.Domain.user;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForumWebApp.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;

        private readonly SignInManager<User> _signInManager;

        public TopicController(ITopicService topicService, SignInManager<User> signInManager)
        {
            _topicService = topicService;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Detail(int id, CancellationToken token = default)
        {
            var topic = await _topicService.GetTopicByIdAsync(id, token);
            return View(topic);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TopicCreateModel topic, CancellationToken token = default)
        {
            if (ModelState.IsValid)
            {

                var currentUser = HttpContext.User;
                var curUserId = int.Parse(_signInManager.UserManager.GetUserId(currentUser));

                await _topicService.CreateTopicAsync(token, topic, curUserId);
                return RedirectToAction("Index", "Home");

            }
            return View(topic);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken token = default)
        {
            var topic = await _topicService.GetTopicByIdAsync(id, token);
            var updatedTopic = topic.Adapt<TopicUpdateModel>();

            return View(updatedTopic);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TopicUpdateModel updateModel, CancellationToken token = default)
        {
            var currentUser = HttpContext.User;
            var curUserId = int.Parse(_signInManager.UserManager.GetUserId(currentUser));

            await _topicService.UpdateTopicAsync(token, updateModel, id, curUserId);
            return RedirectToAction("Detail", new { id });
        }

        public async Task<IActionResult> Delete(int id, CancellationToken token = default)
        {
            var topic = await _topicService.GetTopicByIdAsync(id, token);
            return View(topic);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteTopic(int id, CancellationToken token = default)
        {
            var currentUser = HttpContext.User;
            var curUserId = int.Parse(_signInManager.UserManager.GetUserId(currentUser));

            await _topicService.DeleteTopicAsync(token, id, curUserId);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ChangeStatus(int id, string status, CancellationToken token = default)
        {
            bool active = status == "Active";
            await _topicService.ChangeStatusAsync(token, id, active);
            return RedirectToAction("Topics", "Admin");
        }

        public async Task<IActionResult> ChangeState(int id, string state, CancellationToken token = default)
        {
            bool hide = state == "Hide";
            await _topicService.ChangeStateAsync(token, id, hide);
            return RedirectToAction("Topics", "Admin");
        }
    }
}
