using Forum.Domain.topic;
using Forum.Domain.user;


namespace Forum.Domain.comment
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int AuthorId { get; set; }
        public int TopicId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        
        // Navigation property
        public User? Author { get; set; }
        public Topic? Topic { get; set; }
    }
}
