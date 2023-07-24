namespace Api.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string Message) : base(Message) { }
    }
}
