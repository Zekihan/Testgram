namespace Testgram.Core.Exceptions
{
    public class FollowExistsException : DBException
    {
        public FollowExistsException() : base("This follow already exists.")
        {
        }
    }
}