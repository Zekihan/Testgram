namespace Testgram.Core.Exceptions
{
    public class UserNotExistsException : DBException
    {
        public UserNotExistsException() : base("This user doesn't exists.")
        {
        }
    }
}