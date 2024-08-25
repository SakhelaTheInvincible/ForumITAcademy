using Forum.Application.MainTopics;
using Forum.Domain.topic;
using Forum.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using static Forum.Domain.enums.Enums;

namespace Forum.Infrastructure.Topics
{
    public class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        public TopicRepository(ForumManagementIdentityContext context) : base(context)
        {
        }

        public async Task CreateTopicAsync(CancellationToken cancellationToken, Topic topic)
        {
            await base.AddAsync(cancellationToken, topic);
        }

        public async Task DeleteTopicAsync(CancellationToken cancellationToken, int id)
        {
            await base.RemoveAsync(cancellationToken, id);
        }

        public async Task<bool> Exists(CancellationToken cancellationToken, int id)
        {
            return await base.AnyAsync(cancellationToken, x => x.Id == id);
        }
        public async Task<List<Topic>> GetAllTopicsAsync(CancellationToken cancellation)
        {
            return await _dbSet.Include(t => t.Author)
                .Include(t => t.Comments)
                .ToListAsync(); 
        }
        public async Task<List<Topic>> GetShowingTopics(CancellationToken cancellation)
        {
            return await _dbSet
                .Include(t => t.Author)
                .Include(t => t.Comments)
                .Where(t => t.State == State.Show)
                .ToListAsync(cancellation);
        }

        public async Task<Topic> GetTopicAsync(CancellationToken cancellationToken, int id)
        {
            return _dbSet.Include(t => t.Author)
                        .Include(t => t.Comments)
                        .FirstOrDefault(t => t.Id == id)!;
        }

        public async Task<List<Topic>> GetTopicsByAuthor(CancellationToken cancellation, int authorId)
        {
            return await _dbSet.Include(t => t.Comments)
                         .Where(t => t.AuthorId == authorId).ToListAsync();
        }

        public async Task UpdateTopicAsync(CancellationToken cancellationToken, Topic topic)
        {
            await base.UpdateAsync(cancellationToken, topic);    
        }

        public async Task<List<Topic>> GetOldTopicsAsync(CancellationToken cancellation, int days)
        {
            DateTime thresholdDate = DateTime.Now.AddDays(-days);

            var topics = await _dbSet.Where(t =>
                t.Status == Status.Active && t.Comments != null && t.Comments.Max(c => c.CreatedAt) < thresholdDate
            ).ToListAsync();

            return topics;
        }
    }
}
