namespace TalkNest.Application.Exceptions
{
    public class DuplicatedEmailRequestException : CustomException
    {
        public DuplicatedEmailRequestException(string message) : base(message)
        {

        }
    }

}