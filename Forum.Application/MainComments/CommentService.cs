using Forum.Application.Exceptions;
using Forum.Application.MainTopics;
using Forum.Application.MainUsers;
using Forum.Domain.comment;
using Forum.Domain.user;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.Design;
using System.Threading;


namespace Forum.Application.MainComments
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly ITopicRepository _topicRespository;
        private readonly UserManager<User> _userManager;

        public CommentService(ICommentRepository repository, ITopicRepository topicRespository, UserManager<User> userManager)
        {
            _repository = repository;
            _topicRespository = topicRespository;
            _userManager = userManager;
        }

        public async Task CreateCommentAsync(CancellationToken cancellationToken, CommentCreateModel comment, int authorId, int topicId)
        {
            var mycomment = comment.Adapt<Comment>();

            if (!await _topicRespository.Exists(cancellationToken, topicId))
                throw new TopicNotFoundException();

            if (await _userManager.FindByIdAsync(authorId.ToString()) == null)
                throw new UserNotFoundException();

            var topic = await _topicRespository.GetTopicAsync(cancellationToken, topicId);
            if (topic.Status == Domain.enums.Enums.Status.Inactive)
                throw new InactiveTopicException();

            mycomment.AuthorId = authorId;
            mycomment.TopicId = topicId;
            
            await _repository.CreateCommentAsync(cancellationToken, mycomment);
        }

        public async Task DeleteCommentAsync(CancellationToken cancellationToken, int commentId, int authorId)
        {
            if (!await _repository.Exists(cancellationToken, commentId))
                throw new CommentNotFoundException();

            var mycomment = await _repository.GetCommentByIdAsync(cancellationToken, commentId);

            if (mycomment.AuthorId != authorId)
                throw new OtherTopicException();

            await _repository.DeleteCommentAsync(cancellationToken, commentId);
        }

        public async Task<CommentResponseModel> GetCommentByIdAsync(CancellationToken cancellation, int commentId)
        {
            if (!await _repository.Exists(cancellation, commentId))
                throw new CommentNotFoundException();

            var result = await _repository.GetCommentByIdAsync(cancellation, commentId);
            return result.Adapt<CommentResponseModel>();
        }

        public async Task<List<CommentResponseModel>> GetCommentsByTopic(CancellationToken cancellationToken, int topicId)
        {
            if (!await _topicRespository.Exists(cancellationToken, topicId))
                throw new TopicNotFoundException();

            var comments = await _repository.GetCommentsByTopic(cancellationToken, topicId);
            return comments.Adapt<List<CommentResponseModel>>();
        }

        public async Task UpdateCommentAsync(CancellationToken cancellationToken, CommentUpdateModel comment, int commentId, int authorId)
        {
            if (!await _repository.Exists(cancellationToken, commentId))
                throw new CommentNotFoundException();

            var mycomment = await _repository.GetCommentByIdAsync(cancellationToken, commentId);

            if (mycomment.AuthorId != authorId)
                throw new OtherCommentException();

            mycomment.Content = comment.Content;
            mycomment.ModifiedAt = DateTime.Now;

            await _repository.UpdateCommentAsync(cancellationToken, mycomment);
        }
    }
}
