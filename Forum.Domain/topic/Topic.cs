using Forum.Domain.comment;
using Forum.Domain.user;
using static Forum.Domain.enums.Enums;

namespace Forum.Domain.topic
{
    public class Topic
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Status Status { get; set; } = Status.Active;
        public State State { get; set; } = State.Pending;

        // Navigation property
        public User? Author { get; set; } 
        public List<Comment>? Comments { get; set; } 
    }
}
