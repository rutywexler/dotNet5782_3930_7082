using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DalObject.DataSource;

namespace DalObject
{
    class DalObject
    {
        public DalObject()
        {
            Initalize();  
        }
        public static void InsertStation(Stations station)
        {
            DataSource.BaseStations[DataSource.Config.NumOfBaseStations++] = station;
        }

    }
}
