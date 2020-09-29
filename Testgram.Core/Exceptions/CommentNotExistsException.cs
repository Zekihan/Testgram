using System;
using System.Collections.Generic;
using System.Text;

namespace Testgram.Core.Exceptions
{
    public class CommentNotExistsException : DBException
    {
        public CommentNotExistsException() : base("This comment doesn't exists.") { }
    }
}