using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using IDAL;

namespace BL
{
    public partial class Bl:BL
    {
        private IDal dal;
        private List<DroneToList> drones;
        public Bl()
        {
            dal = new DalObject.DalObject();
            drones = new List<DroneToList>();
        }
    }
}
