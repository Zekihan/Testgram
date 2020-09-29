namespace Testgram.Core.Exceptions
{
    public class CommentNotExistsException : DBException
    {
        public CommentNotExistsException() : base("This comment doesn't exists.")
        {
        }
    }
}