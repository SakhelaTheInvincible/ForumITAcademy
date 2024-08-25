namespace Forum.Application.Exceptions
{
    public class OtherTopicException : Exception
    {
        public OtherTopicException() : base() { }
        public OtherTopicException(string message) : base(message) { }
        public readonly string Code = "You Can't Modify Other User's Topics";
    }
}
