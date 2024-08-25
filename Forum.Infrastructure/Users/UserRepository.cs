using Forum.Application.MainUsers;
using Forum.Domain.user;
using Forum.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ForumManagementIdentityContext context) : base(context) 
        {
            
        }

        public async Task<List<User>> GetAllUserAsync(CancellationToken cancellationToken)
        {
            return await GetAllAsync(cancellationToken);
        }

        public async Task<int> CommentCount(int userId)
        {
            var user = await _dbSet.Include(u => u.Comments)!.ThenInclude(u => u.Topic).FirstOrDefaultAsync(u => u.Id == userId);

            return user!.Comments != null ? user.Comments.Count(c => c.Topic != null && c.Topic.AuthorId != userId) : 0;
        }

        public async Task<List<User>> UnbanUsersAsync(CancellationToken cancellation, int days)
        {
            DateTime thresholdDate = DateTime.Now.AddDays(-days);

            var topics = await _dbSet.Where(u =>
               u.IsBanned && u.ModifiedAt <= thresholdDate
            ).ToListAsync();

            return topics;
        }
    }
}
