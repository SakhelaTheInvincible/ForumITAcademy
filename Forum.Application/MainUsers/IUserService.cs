using Forum.Domain.user;

namespace Forum.Application.MainUsers
{
    public interface IUserService
    {
        Task<User> AuthenticationAsync(UserLoginModel userLogin);
        Task<List<UserAdminResponseModel>> GetAllUserAsync(CancellationToken cancellationToken);
        Task<UserResponseModel> GetUserByIdAsync(int userId);
        Task<UserPageResponseModel> GetUserByEmailAsync(CancellationToken cancellationToken, string email);
        Task CreateUserAsync(UserCreateModel user);
        Task UpdateUsercAsync(int userId, UserUpdateModel user);
        Task DeleteUserAsync(int userId);
        Task BanUserAsync(int userId, bool banned);
        Task<int> CommentCount(int userId);
        Task<List<User>> UnbanUsersAsync(CancellationToken cancellationToken, int days);
        Task LogOut();
    }
}
