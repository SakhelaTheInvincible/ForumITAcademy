using Forum.Application.MainTopics;
using Forum.Application.MainUsers;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.API.Controllers
{
    [Route("v1/User")]
    [Authorize(Policy = "User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITopicService _topicService;


        public UserController(IUserService service, ITopicService topicService)
        {
            _userService = service;
            _topicService = topicService;
        }

        [HttpGet("email/{email}")]
        public async Task<UserPageResponseModelAPI> GetUserByEmail(CancellationToken cancellationToken, string email)
        {
            var result = await _userService.GetUserByEmailAsync(cancellationToken, email);
            return new UserPageResponseModelAPI { User = result.User.Adapt<UserResponseModel>(), Topics = result.Topics.Adapt<List<TopicResponseModelAPI>>()};
        }

        [HttpGet("id/{id}")]
        public async Task<UserPageResponseModelAPI> GetOtherUser(int id, CancellationToken cancellation = default)
        {
            var user = await _userService.GetUserByIdAsync(id);
            var list = await _topicService.GetTopicsByAuthorAsync(cancellation, id);
            return new UserPageResponseModelAPI { User = user, Topics = list.Adapt<List<TopicResponseModelAPI>>() };
        }


        [Route("logout")]
        [HttpPost]
        public async Task LogOut()
        {
            await _userService.LogOut();
        }

        [Route("profile")]
        [HttpGet]
        public async Task<UserPageResponseModelAPI> GetSelf(CancellationToken cancellation = default)
        {
            int Id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            return await GetOtherUser(Id, cancellation);
        }

        [Route("profile")]
        [HttpPut]
        public async Task Put(UserUpdateModel user)
        {
            int Id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _userService.UpdateUsercAsync(Id, user);
        }

        [Route("profile")]
        [HttpDelete]
        public async Task Delete()
        {
            var Id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _userService.DeleteUserAsync(Id);
        }
    }
}
