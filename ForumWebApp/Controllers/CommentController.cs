using Forum.Application.MainComments;
using Forum.Application.MainTopics;
using Forum.Domain.user;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace ForumWebApp.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commmentService;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentController(ICommentService commentService, ITopicService topicService, IHttpContextAccessor httpContextAccessor, SignInManager<User> signInManager)
        {
           // _topicService = topicService;
            _commmentService = commentService;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, CommentCreateModel comment, CancellationToken token = default)
        {
            if (ModelState.IsValid)
            {

                var currentUser = HttpContext.User;
                var curUserId = int.Parse(_signInManager.UserManager.GetUserId(currentUser));

                await _commmentService.CreateCommentAsync(token, comment, curUserId, id);
                return RedirectToAction("Detail", "Topic", new { id = id });

            }
            return View(comment);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken token = default)
        {

            var comment = await _commmentService.GetCommentByIdAsync(token, id);
            var updatedTopic = comment.Adapt<CommentUpdateModel>();

            return View(updatedTopic);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CommentUpdateModel updateModel, CancellationToken token = default)
        {
            var currentUser = HttpContext.User;
            var curUserId = int.Parse(_signInManager.UserManager.GetUserId(currentUser));

            await _commmentService.UpdateCommentAsync(token, updateModel, id, curUserId);

            var comment = await _commmentService.GetCommentByIdAsync(token, id);

            return RedirectToAction("Detail", "Topic", new {id = comment.TopicId});
        }

        public async Task<IActionResult> Delete(int id, CancellationToken token = default)
        {
            var topic = await _commmentService.GetCommentByIdAsync(token, id);
            return View(topic);
        }
         
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteComment(int id, CancellationToken token = default)
        {
            var currentUser = HttpContext.User;
            var curUserId = int.Parse(_signInManager.UserManager.GetUserId(currentUser));

            var comment = await _commmentService.GetCommentByIdAsync(token, id);

            await _commmentService.DeleteCommentAsync(token, id, curUserId);
            return RedirectToAction("Detail", "Topic", new {id = comment.TopicId});
        }
    }
}
