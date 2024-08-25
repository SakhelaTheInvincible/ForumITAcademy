using Forum.Application.MainComments;

namespace Forum.Application.MainTopics
{
    public class TopicPageResponseModel
    {
        public TopicResponseModelAPI? Topic {  get; set; }
        public List<CommentResponseModelAPI>? Comments { get; set; } 
    }
}
