namespace Forum.Application.MainComments
{
    public interface ICommentService
    {
        Task<List<CommentResponseModel>> GetCommentsByTopic(CancellationToken cancellation, int topicId);
        Task<CommentResponseModel> GetCommentByIdAsync(CancellationToken cancellation, int id);
        Task CreateCommentAsync(CancellationToken cancellationToken, CommentCreateModel comment, int authorId, int topicId);
        Task UpdateCommentAsync(CancellationToken cancellationToken, CommentUpdateModel comment, int commentId, int authorId);
        Task DeleteCommentAsync(CancellationToken cancellationToken, int commentId, int authorId);
    }
}
