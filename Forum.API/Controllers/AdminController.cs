using Forum.Application.MainTopics;
using Forum.Application.MainUsers;
using Forum.Domain.enums;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Route("v1/admin")]
    [Authorize(Policy = "Administrator")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITopicService _topicService;

        public AdminController(IUserService userService, ITopicService topicService)
        {
            _topicService = topicService;
            _userService = userService;
        }

        [Route("topics")]
        [HttpGet]
        public async Task<List<TopicAdminResponseModelAPI>> GetTopics(CancellationToken cancellation = default)
        {
            var result = await _topicService.GetAllTopicsAsync(cancellation);
            return result.Adapt<List<TopicAdminResponseModelAPI>>();
        }

        [Route("topic/{topicId}/state/{hide}")]
        [HttpPut]
        public async Task ChangeState(int topicId, bool hide, CancellationToken cancellation = default)
        {
            await _topicService.ChangeStateAsync(cancellation, topicId, hide);
        }

        [Route("topic/{topicId}/status/{active}")]
        [HttpPut]
        public async Task ChangeStatus(int topicId, bool active, CancellationToken cancellation = default)
        {
            await _topicService.ChangeStatusAsync(cancellation, topicId, active);
        }



        [Route("users")]
        [HttpGet]
        public async Task<List<UserAdminResponseModelAPI>> GetUsers(CancellationToken cancellation = default)
        {
            var result = await _userService.GetAllUserAsync(cancellation);
            return result.Adapt<List<UserAdminResponseModelAPI>>();
        }        
        
        [Route("user/{userId}/ban/{ban}")]
        [HttpPut]
        public async Task ChangeBan(int userId, bool ban)
        {
            await _userService.BanUserAsync(userId, ban);
        }
    }
}
