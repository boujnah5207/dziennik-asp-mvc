using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace dziennik_asp_mvc.Exceptions
{
    public class SubjectNotFoundException : Exception, ISerializable
    {
        public SubjectNotFoundException()
        {
            // Add implementation.
        }
        public SubjectNotFoundException(string message)
            : base(message)
        {
            // Add implementation.
        }
        public SubjectNotFoundException(string message, Exception inner)
            : base(
                message, inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected SubjectNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation.
        }
    }
}