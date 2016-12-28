using System;
using System.Runtime.Serialization;

namespace Contracts
{
    [Serializable]
    public class ContractNotEqualsException : Exception
    {
        public ContractNotEqualsException()
        { }

        public ContractNotEqualsException(string message)
            : base(message)
        { }

        public ContractNotEqualsException(string message, Exception inner)
            : base(message, inner)
        { }

        protected ContractNotEqualsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

    }
}
