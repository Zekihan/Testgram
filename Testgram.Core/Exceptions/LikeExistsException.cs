namespace Testgram.Core.Exceptions
{
    public class LikeExistsException : DBException
    {
        public LikeExistsException() : base("This like already exists.")
        {
        }
    }
}