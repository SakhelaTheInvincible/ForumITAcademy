namespace Forum.Application.Exceptions
{
    public class TopicNotFoundException : Exception
    {
        public TopicNotFoundException() : base() { }
        public TopicNotFoundException(string message) : base(message) { }

        public readonly string Code = "Topic Not Found";
    }
}
