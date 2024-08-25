namespace Forum.Application.Exceptions
{
    public class OtherCommentException : Exception
    {
        public OtherCommentException() : base() { }
        public OtherCommentException(string message) : base(message) { }
        public readonly string Code = "You Can't Modify Other User's Comments";
    }
}
