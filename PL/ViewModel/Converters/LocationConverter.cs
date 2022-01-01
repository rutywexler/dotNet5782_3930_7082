using PL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Converters
{
    public static class LocationConverter
    {
        public static  BlApi.IBL bl = BlApi.BlFactory.GetBL();

        public static BO.Location ConvertBackLocation(Location location)
        {
            return new BO.Location()
            {
                Longitude = (double)location.Longitude,
                Lattitude = (double)location.Latitude
            };
        }

        public static Location ConvertLocation(BO.Location location)
        {
            return new Location()
            {
                Longitude = (double)location.Longitude,
                Latitude = (double)location.Lattitude
            };
        }
    }
}
