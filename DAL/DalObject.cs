using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DalObject.DataSource;
using static DalObject.DataSource.Config;

namespace DalObject
{
    public class DalObject
    {
        public DalObject()
        {
            Initalize();  
        }
        public void InsertStation(Stations station)
        {
            DataSource.BaseStations[DataSource.Config.NumOfBaseStations++] = station;
        }

    }
}
