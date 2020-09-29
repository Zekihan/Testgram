namespace Testgram.Core.Exceptions
{
    public class DBException : System.Exception
    {
        public DBException() : base()
        {
        }

        public DBException(string message) : base(message)
        {
        }

        public DBException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected DBException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}