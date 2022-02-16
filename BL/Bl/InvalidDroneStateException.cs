using System;
using System.Runtime.Serialization;

namespace BL
{
    [Serializable]
    public class InvalidDroneStateException : Exception
    {
        public InvalidDroneStateException()
        {
        }

        public InvalidDroneStateException(string message) : base(message)
        {
        }

        public InvalidDroneStateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidDroneStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}