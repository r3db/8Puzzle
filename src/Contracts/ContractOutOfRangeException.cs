using System;
using System.Runtime.Serialization;

namespace Contracts
{
    [Serializable]
    public class ContractOutOfRangeException : Exception
    {
        public ContractOutOfRangeException()
        { }

        public ContractOutOfRangeException(string message) : base(message)
        { }

        public ContractOutOfRangeException(string message, Exception inner) : base(message, inner)
        { }

        protected ContractOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    
    }
}
