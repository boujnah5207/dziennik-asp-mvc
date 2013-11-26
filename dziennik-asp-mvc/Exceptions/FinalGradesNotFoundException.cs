using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace dziennik_asp_mvc.Exceptions
{
    public class FinalGradesNotFoundException : Exception, ISerializable
    {
        public FinalGradesNotFoundException()
        {
            // Add implementation.
        }
        public FinalGradesNotFoundException(string message)
            : base(message)
        {
            // Add implementation.
        }
        public FinalGradesNotFoundException(string message, Exception inner)
            : base(
                message, inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected FinalGradesNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation.
        }
    }
}