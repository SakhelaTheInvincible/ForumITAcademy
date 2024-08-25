using Forum.Application.MainComments;
using Forum.Domain.comment;
using Forum.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Comments
{
    public class CommmentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommmentRepository(ForumManagementIdentityContext context) : base(context) 
        {
            
        }

        public async Task CreateCommentAsync(CancellationToken cancellationToken, Comment comment)
        {
            await base.AddAsync(cancellationToken, comment);
        }


        public async Task DeleteCommentAsync(CancellationToken cancellationToken, int id)
        {
            await base.RemoveAsync(cancellationToken, id);
        }

        public async Task<bool> Exists(CancellationToken cancellationToken, int id)
        {
            return await base.AnyAsync(cancellationToken, x => x.Id == id);
        }

        public async Task<Comment> GetCommentByIdAsync(CancellationToken cancellationToken, int id)
        {
            return await base.GetAsync(cancellationToken, id);
        }

        public async Task<List<Comment>> GetCommentsByTopic(CancellationToken cancellation, int topicid)
        {
            return await _dbSet.Include(c => c.Author).Include(c => c.Topic).Where(x => x.TopicId == topicid).ToListAsync();
        }

        public async Task UpdateCommentAsync(CancellationToken cancellationToken, Comment comment)
        {
            await base.UpdateAsync(cancellationToken, comment);
        }

    }
}
