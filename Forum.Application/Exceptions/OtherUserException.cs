namespace Forum.Application.Exceptions
{
    public class OtherUserException : Exception
    {
        public OtherUserException() : base() { }
        public OtherUserException(string message) : base(message) { }
        public readonly string Code = "You Can't Modify Other User's Information";
    }
}
