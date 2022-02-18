﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    [Serializable]
    public class ThereIsAnotherObjectWithThisUniqueID : Exception
    {
        public ThereIsAnotherObjectWithThisUniqueID() : base() { }
        public ThereIsAnotherObjectWithThisUniqueID(string message) : base(message) { }
        public ThereIsAnotherObjectWithThisUniqueID(string message, Exception inner) : base(message, inner) { }
        protected ThereIsAnotherObjectWithThisUniqueID(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public class DalConfigException : Exception
    {
        public DalConfigException(string message) : base(message) { }
        public DalConfigException(string message, Exception inner) : base(message, inner) { }
    }


    [Serializable]
    public class Exception_ThereIsInTheListObjectWithTheSameValue : Exception
    {
        public Exception_ThereIsInTheListObjectWithTheSameValue() : base() { }
        public Exception_ThereIsInTheListObjectWithTheSameValue(string massage) : base(massage) { }

        public override string ToString()
        {
            return Message + "Exception! there is in the list object with the same value";
        }
    }


}
