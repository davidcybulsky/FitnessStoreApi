namespace Api.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string Message) : base(Message) { }
    }
}
