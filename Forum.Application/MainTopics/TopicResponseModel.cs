using Forum.Domain.enums;
using static Forum.Domain.enums.Enums;

namespace Forum.Application.MainTopics
{
    public class TopicResponseModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public int AuthorId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int CommentCount { get; set; }
        public string? Status { get; set; }
    }
}
