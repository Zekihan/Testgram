using System;
using System.Collections.Generic;
using System.Text;

namespace Testgram.Core.Exceptions
{
    public class UserNotExistsException : DBException
    {
        public UserNotExistsException() : base("This user doesn't exists.") { }
    }
}