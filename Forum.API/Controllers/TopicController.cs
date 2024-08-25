using Forum.Application.MainComments;
using Forum.Application.MainTopics;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.API.Controllers
{
    [Route("v1/topic")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly ICommentService _commentService;
        public TopicController(ITopicService topicService, ICommentService commentService)
        {
            _commentService = commentService;
            _topicService = topicService;
        }

        [Authorize(Policy = "User")]
        [HttpPost]
        public async Task Create(TopicCreateModel topic, CancellationToken cancellation = default)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _topicService.CreateTopicAsync(cancellation, topic, id);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<TopicPageResponseModel> Get(int id, CancellationToken cancellation = default)
        {
            var topic = await _topicService.GetTopicByIdAsync(id, cancellation);
            var list = await _commentService.GetCommentsByTopic(cancellation, id);
            topic.CommentCount = list.Count;
            return new TopicPageResponseModel { Topic = topic.Adapt<TopicResponseModelAPI>(), Comments = list.Adapt<List<CommentResponseModelAPI>>()};
        }

        [Authorize(Policy = "User")]
        [HttpPut("{id}")]
        public async Task Put(int id, TopicUpdateModel topic, CancellationToken cancellation)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _topicService.UpdateTopicAsync(cancellation, topic, id, userId);
        }

        [Authorize(Policy = "User")]
        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _topicService.DeleteTopicAsync(cancellationToken, id, userId);
        }

    }
}
