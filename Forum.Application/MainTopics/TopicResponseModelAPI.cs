namespace Forum.Application.MainTopics
{
    public class TopicResponseModelAPI
    {
        public string? UserName { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int CommentCount { get; set; }
        public string? Status { get; set; }
    }
}
