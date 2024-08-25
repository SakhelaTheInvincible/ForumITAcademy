
using Forum.Application.Exceptions;
using Forum.Application.MainTopics;
using Forum.Domain.user;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Forum.Application.MainUsers
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITopicRepository _topicRepository;

        public UserServices(IUserRepository repository, UserManager<User> userManager, SignInManager<User> signInManager, ITopicRepository topicRepository)
        {
            _repository = repository;
            _userManager = userManager;
            _signInManager = signInManager;
            _topicRepository = topicRepository;
        }

        public async Task<User> AuthenticationAsync(UserLoginModel userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin.UserName);

            var response = await _signInManager.PasswordSignInAsync(user, userLogin.Password, false, false);

            if (!response.Succeeded)
                throw new LoginException();

            if (user.IsBanned)
                throw new UserBannedException();

            return user;
        }

        public async Task BanUserAsync(int userId, bool banned)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException();

            user.IsBanned = banned;
            user.ModifiedAt = DateTime.Now;

            await _userManager.UpdateAsync(user);
        }

        public async Task<int> CommentCount(int userId)
        {
            return await _repository.CommentCount(userId);
        }

        public async Task CreateUserAsync(UserCreateModel user)
        {
            

            var check = await _userManager.FindByNameAsync(user.UserName);

            if (check != null) 
                throw new UserNameAlreadyExistsException();

            check = await _userManager.FindByEmailAsync(user.Email);

            if (check != null)
                throw new EmailAlreadyExistsException();

            var newUser = user.Adapt<User>();
            newUser.NormalizedEmail = user.Email!.ToUpper();
            newUser.NormalizedUserName = user.UserName!.ToUpper();

            await _userManager.CreateAsync(newUser, user.Password);
            await _userManager.AddToRoleAsync(newUser, "User");

        }

        public async Task DeleteUserAsync(int userId)
        {

            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException();

            await _signInManager.SignOutAsync();

            await _userManager.DeleteAsync(user);
        }

        public async Task<List<UserAdminResponseModel>> GetAllUserAsync(CancellationToken cancellationToken)
        {

            var users = await _repository.GetAllUserAsync(cancellationToken);

            return users.Adapt<List<UserAdminResponseModel>>();
        }

        public async Task<UserPageResponseModel> GetUserByEmailAsync(CancellationToken cancellationToken, string email)
        {
            var user = await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException();

            var topics = await _topicRepository.GetTopicsByAuthor(cancellationToken, user.Id);

            var userResponse = user.Adapt<UserResponseModel>();
            userResponse.CommentsOnOtherTopicsCount = await _repository.CommentCount(user.Id);

            return new UserPageResponseModel
            {
                User = user,
                UserResponse = userResponse,
                Topics = topics.Adapt<List<TopicResponseModel>>()
            };
        }

        public async Task<UserResponseModel> GetUserByIdAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)   
                throw new UserNotFoundException();

            var result = user.Adapt<UserResponseModel>();
            result.CommentsOnOtherTopicsCount = await _repository.CommentCount(userId);

            return result;
        }

        public async Task UpdateUsercAsync(int userId, UserUpdateModel user)
        {

            var oldUser = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException();

            user.Adapt(oldUser);

            oldUser.ModifiedAt = DateTime.Now;

            await _userManager.UpdateAsync(oldUser);
        }

        public async Task<List<User>> UnbanUsersAsync(CancellationToken cancellationToken, int days)
        {
            return await _repository.UnbanUsersAsync(cancellationToken, days);
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();

        }
    }
}
