using Forum.Domain.comment;


namespace Forum.Application.MainComments
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsByTopic(CancellationToken cancellation, int topicid);
        Task CreateCommentAsync(CancellationToken cancellationToken,Comment comment);
        Task UpdateCommentAsync(CancellationToken cancellationToken,Comment comment);
        Task DeleteCommentAsync(CancellationToken cancellationToken,int id);
        Task<bool> Exists(CancellationToken cancellationToken, int id);
        Task<Comment> GetCommentByIdAsync(CancellationToken cancellationToken, int id);

    }
}
