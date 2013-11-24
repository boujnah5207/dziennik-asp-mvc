using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace dziennik_asp_mvc.Exceptions
{
    public class GroupNotFoundException : Exception, ISerializable
    {
        public GroupNotFoundException()
        {
            // Add implementation.
        }
        public GroupNotFoundException(string message)
            : base(message)
        {
            // Add implementation.
        }
        public GroupNotFoundException(string message, Exception inner)
            : base(
                message, inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected GroupNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation.
        }
    }
}