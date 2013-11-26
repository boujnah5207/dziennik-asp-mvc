using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace dziennik_asp_mvc.Exceptions
{
    public class CreditingFormNotFoundException : Exception, ISerializable
    {
        public CreditingFormNotFoundException()
        {
            // Add implementation.
        }
        public CreditingFormNotFoundException(string message)
            : base(message)
        {
            // Add implementation.
        }
        public CreditingFormNotFoundException(string message, Exception inner)
            : base(
                message, inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected CreditingFormNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation.
        }
    }
}