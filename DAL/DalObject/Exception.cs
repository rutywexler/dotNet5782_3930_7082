using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalObject
{

    [Serializable]
    public class Exception_ThereIsInTheListObjectWithTheSameValue:Exception
    {
        public Exception_ThereIsInTheListObjectWithTheSameValue() : base() { }
        public Exception_ThereIsInTheListObjectWithTheSameValue(string massage): base(massage) { }

        public override string ToString()
        {
            return Message + "Exception! there is in the list object with the same value";
        }
    }

   

}
