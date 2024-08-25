using Forum.Domain.topic;

namespace Forum.Application.MainTopics
{
    public interface ITopicService
    {
        Task<List<TopicResponseModel>> GetShowingTopicsAsync(CancellationToken cancellation);
        Task <List<TopicResponseModel>> GetTopicsByAuthorAsync(CancellationToken cancellation, int authorId);
        Task<List<TopicAdminResponseModel>> GetAllTopicsAsync(CancellationToken cancellation);
        Task<TopicResponseModel> GetTopicByIdAsync(int id, CancellationToken cancellation);
        Task CreateTopicAsync(CancellationToken cancellationToken, TopicCreateModel topic, int authorId);
        Task UpdateTopicAsync(CancellationToken cancellationToken, TopicUpdateModel topic, int topicId, int authorId);
        Task DeleteTopicAsync(CancellationToken cancellationToken, int topicId, int authorId);
        Task ChangeStateAsync(CancellationToken cancellationToken, int topicId, bool hide);
        Task ChangeStatusAsync(CancellationToken cancellationToken, int topicId, bool active);
        Task<List<Topic>> GetOldTopicsAsync(CancellationToken cancellation, int days);
    }
}
