using Forum.API.Infrastructure.Auth.JWT;
using Forum.Application.MainTopics;
using Forum.Application.MainUsers;
using Forum.Domain.user;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Forum.API.Controllers
{
    [Route("v1/news_feed")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITopicService _topicService;
        private readonly UserManager<User> _userManager;
        private readonly IOptions<JWTConfiguration> _options;


        public GuestController(IUserService service, ITopicService topicService, IOptions<JWTConfiguration> options, UserManager<User> userManager)
        {
            _userService = service;
            _topicService = topicService;
            _options = options;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<List<TopicResponseModelAPI>> GetTopics(CancellationToken cancellation = default)
        {
            var result = await _topicService.GetShowingTopicsAsync(cancellation);
            return result.Adapt<List<TopicResponseModelAPI>>();
        }

        [Authorize(Policy = "GuestOnly")]
        [Route("registration")]
        [HttpPost] 
        public async Task Register(UserCreateModel user)
        {
            await _userService.CreateUserAsync(user);
        }

        [Authorize(Policy = "GuestOnly")]
        [Route("authorization")]
        [HttpPost]
        public async Task<string> LogIn(UserLoginModel user)
        {
            var result = await _userService.AuthenticationAsync(user);
            var roles = await _userManager.GetRolesAsync(result);
            return JWTHelper.GenerateSecurityToken(result.Id, roles, _options);
        }

    }
}
