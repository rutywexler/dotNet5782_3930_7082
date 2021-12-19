using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{
    [Serializable]
    public class Exception_NotExistCloseStationForTheDrone : Exception
    {
        public Exception_NotExistCloseStationForTheDrone() : base() { }
        public Exception_NotExistCloseStationForTheDrone(string message) : base(message) { }

        public override string ToString()
        {
            return Message + "Exception! not exist close ststion for the drone";
        }

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

    [Serializable]
    internal class InValidActionException : Exception
    {
        public InValidActionException()
        {
        }

        public InValidActionException(string message) : base(message)
        {
        }

    }
}
