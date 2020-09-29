using System;
using System.Collections.Generic;
using System.Text;

namespace Testgram.Core.Exceptions
{
    public class PostNotExistsException : DBException
    {
        public PostNotExistsException() : base("This post doesn't exists.") { }
    }
}