using System;
using System.Collections.Generic;
using System.Text;

namespace Testgram.Core.Exceptions
{
    public class FollowExistsException : DBException
    {
        public FollowExistsException() : base("This follow already exists.") { }
    }
}