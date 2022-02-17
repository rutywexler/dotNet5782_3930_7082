using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{

    [Serializable]
    public class excepti:Exception
    {
        public excepti() : base() { }
        public excepti(string massage): base(massage) { }

        public override string ToString()
        {
            return Message + "Exception! there is in the list object with the same value";
        }
    }

   

}
