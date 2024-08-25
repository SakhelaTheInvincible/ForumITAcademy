namespace Forum.Application.Exceptions
{
    public class CommentCountException : Exception
    {
        public CommentCountException() : base() { }
        public CommentCountException(string message) : base(message) { }

        public readonly string Code = "Comment Not Found";
    }
}
