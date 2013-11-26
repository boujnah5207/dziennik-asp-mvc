using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace dziennik_asp_mvc.Exceptions
{
    public class PartialGradesNotFoundException : Exception, ISerializable
    {
        public PartialGradesNotFoundException()
        {
            // Add implementation.
        }
        public PartialGradesNotFoundException(string message)
            : base(message)
        {
            // Add implementation.
        }
        public PartialGradesNotFoundException(string message, Exception inner)
            : base(
                message, inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected PartialGradesNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation.
        }
    }
}