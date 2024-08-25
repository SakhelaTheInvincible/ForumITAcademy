using Forum.Domain.user;

namespace Forum.Application.MainUsers
{
    public interface IUserRepository
    {
        Task <List<User>> GetAllUserAsync(CancellationToken cancellationToken);
        Task<int> CommentCount(int userId);
        Task<List<User>> UnbanUsersAsync(CancellationToken cancellationToken, int days);
    }
}
