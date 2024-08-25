using static Forum.Domain.enums.Enums;

namespace Forum.Application.MainTopics
{
    public class TopicAdminResponseModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int CommentCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserName { get; set; }
        public string? Status { get; set; }
        public string? State { get; set; }

    }
}
