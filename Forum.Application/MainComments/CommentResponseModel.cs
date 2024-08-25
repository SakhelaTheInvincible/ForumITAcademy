namespace Forum.Application.MainComments
{
    public class CommentResponseModel
    {
        public int Id { get; set; } 
        public string? Content { get; set; }
        public string? UserName { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int TopicId { get; set; }
    }
}
