
using Forum.Domain.topic;

    
namespace Forum.Application.MainTopics
{
    public interface ITopicRepository
    {
        Task<List<Topic>> GetShowingTopics(CancellationToken cancellation);
        Task<List<Topic>> GetAllTopicsAsync(CancellationToken cancellation);
        Task<List<Topic>> GetTopicsByAuthor(CancellationToken cancellation, int authorid);
        Task CreateTopicAsync(CancellationToken cancellationToken, Topic topic);
        Task UpdateTopicAsync(CancellationToken cancellationToken, Topic topic);
        Task DeleteTopicAsync(CancellationToken cancellationToken, int id);
        Task<Topic> GetTopicAsync(CancellationToken cancellationToken, int id);
        Task<bool> Exists(CancellationToken cancellationToken, int id);
        Task<List<Topic>> GetOldTopicsAsync(CancellationToken cancellation, int days);
    }
}
