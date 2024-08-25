using Forum.Application.MainComments;
using Forum.Domain.enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.API.Controllers
{
    [Route("v1/topic/{topicId}/comment")]
    [Authorize(Policy = "User")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task CreateComment(int topicId, CommentCreateModel comment, CancellationToken cancellation = default)
        {
            int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _commentService.CreateCommentAsync(cancellation, comment, authorId, topicId);
        }

        [HttpPut("{commentId}")]
        public async Task PutComment(int commentId, CommentUpdateModel comment, CancellationToken cancellation)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _commentService.UpdateCommentAsync(cancellation, comment, commentId, userId);
        }

        [HttpDelete("{commentId}")]
        public async Task DeleteComment(int commentId, CancellationToken cancellationToken)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _commentService.DeleteCommentAsync(cancellationToken, commentId, userId);
        }
    }
}
