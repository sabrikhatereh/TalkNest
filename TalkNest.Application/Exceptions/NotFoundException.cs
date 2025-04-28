namespace TalkNest.Application.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}