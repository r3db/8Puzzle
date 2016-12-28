using System;
using System.Runtime.Serialization;

namespace Contracts
{
    [Serializable]
    public class ContractNotNullException : Exception
    {
        public ContractNotNullException()
        { }

        public ContractNotNullException(string message)
            : base(message)
        { }

        public ContractNotNullException(string message, Exception inner)
            : base(message, inner)
        { }

        protected ContractNotNullException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

    }
}
