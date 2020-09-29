using System;
using System.Collections.Generic;
using System.Text;

namespace Testgram.Core.Exceptions
{
    public class LikeExistsException : DBException
    {
        public LikeExistsException() : base("This like already exists.") { }
    }
}