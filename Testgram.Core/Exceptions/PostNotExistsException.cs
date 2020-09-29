namespace Testgram.Core.Exceptions
{
    public class PostNotExistsException : DBException
    {
        public PostNotExistsException() : base("This post doesn't exists.")
        {
        }
    }
}