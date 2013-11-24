using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace dziennik_asp_mvc.Exceptions
{
    public class UserNameAlreadyExistsException : Exception, ISerializable
    {
        public UserNameAlreadyExistsException()
        {
            // Add implementation.
        }
        public UserNameAlreadyExistsException(string message)
            : base(message)
        {
            // Add implementation.
        }
        public UserNameAlreadyExistsException(string message, Exception inner)
            : base(
                message, inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected UserNameAlreadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation.
        }
    }
}