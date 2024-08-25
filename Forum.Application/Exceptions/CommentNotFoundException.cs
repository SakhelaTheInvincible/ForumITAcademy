namespace Forum.Application.Exceptions
{
    public class CommentNotFoundException : Exception
    {
        public CommentNotFoundException() : base() { }
        public CommentNotFoundException(string message) : base(message) { }

        public readonly string Code = "Comment Not Found";
    }
}
