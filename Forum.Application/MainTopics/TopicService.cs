using Forum.Domain.topic;
using Mapster;
using static Forum.Domain.enums.Enums;
using Forum.Application.Exceptions;
using Microsoft.AspNetCore.Identity;
using Forum.Domain.user;
using Forum.Application.MainUsers;

namespace Forum.Application.MainTopics
{
    public class TopicService : ITopicService
    {   
        private readonly ITopicRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public TopicService(ITopicRepository repository, UserManager<User> userManager, IUserRepository userRepository)
        {
            _repository = repository;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task CreateTopicAsync(CancellationToken cancellationToken, TopicCreateModel topic, int authorId)
        {
            if (await _userManager.FindByIdAsync(authorId.ToString()) == null)
                throw new UserNotFoundException();

            if (await _userRepository.CommentCount(authorId) <= 2)
                throw new CommentCountException();
            
            var mytopic = topic.Adapt<Topic>();

            mytopic.AuthorId = authorId;

            await _repository.CreateTopicAsync(cancellationToken, mytopic);
        }

        public async Task DeleteTopicAsync(CancellationToken cancellationToken, int topicId, int authorId)
        {
            if (!await _repository.Exists(cancellationToken, topicId))
                throw new TopicNotFoundException();

            var myTopic = await _repository.GetTopicAsync(cancellationToken, topicId);

            if (myTopic.AuthorId != authorId)
                throw new OtherTopicException();

            await _repository.DeleteTopicAsync(cancellationToken, topicId);
        }

        public async Task<List<TopicResponseModel>> GetShowingTopicsAsync(CancellationToken cancellation)
        {
            var topics = await _repository.GetShowingTopics(cancellation);
            return topics.Adapt<List<TopicResponseModel>>();
        }

        public async Task<List<TopicResponseModel>> GetTopicsByAuthorAsync(CancellationToken cancellation, int authorId)
        {
            if (await _userManager.FindByIdAsync(authorId.ToString()) == null)
                throw new UserNotFoundException();


            var topics = await _repository.GetTopicsByAuthor(cancellation, authorId);

            return topics.Adapt(new List<TopicResponseModel>());
        }

        public async Task<TopicResponseModel> GetTopicByIdAsync(int topicId, CancellationToken cancellation)
        {
            if (!await _repository.Exists(cancellation, topicId))
                throw new TopicNotFoundException();

            var topic = await _repository.GetTopicAsync(cancellation, topicId);

            return topic.Adapt<TopicResponseModel>();
        }


        public async Task<List<TopicAdminResponseModel>> GetAllTopicsAsync(CancellationToken cancellation)
        {
            var topics = await _repository.GetAllTopicsAsync(cancellation);
            return topics.Adapt<List<TopicAdminResponseModel>>();
        }


        public async Task UpdateTopicAsync(CancellationToken cancellationToken, TopicUpdateModel topic, int topicId, int authorId)
        {
            if (!await _repository.Exists(cancellationToken, topicId))
                throw new TopicNotFoundException();

            var myTopic = await _repository.GetTopicAsync(cancellationToken, topicId);

            topic.Adapt(myTopic);

            if (myTopic.AuthorId != authorId)
                throw new OtherTopicException();

            myTopic.ModifiedAt = DateTime.Now;

            await _repository.UpdateTopicAsync(cancellationToken, myTopic);
        }

        public async Task ChangeStateAsync(CancellationToken cancellationToken, int topicId, bool hide)
        {
            if (!await _repository.Exists(cancellationToken, topicId))
                throw new TopicNotFoundException();

            var myTopic = await _repository.GetTopicAsync(cancellationToken, topicId);

            myTopic.State = hide ? State.Hide : State.Show;

            await _repository.UpdateTopicAsync(cancellationToken, myTopic);
        }

        public async Task ChangeStatusAsync(CancellationToken cancellationToken, int topicId, bool active)
        {
            if (!await _repository.Exists(cancellationToken, topicId))
                throw new TopicNotFoundException();

            var myTopic = await _repository.GetTopicAsync(cancellationToken, topicId);

            myTopic.Status = active ? Status.Active : Status.Inactive;

            await _repository.UpdateTopicAsync(cancellationToken, myTopic);
        }

        public async Task<List<Topic>> GetOldTopicsAsync(CancellationToken cancellation, int days)
        {
            return await _repository.GetOldTopicsAsync(cancellation, days);
        }
    }
}
