using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
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

    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;

        public XMLFileLoadCreateException()
        {
        }

        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }
}
