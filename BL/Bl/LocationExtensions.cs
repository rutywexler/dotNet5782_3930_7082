using BO;
using System;

namespace BL
{
    class LocationExtensions
    {
        public static double Distance(Location sLocation, Location dLocation)
        {
            int R = 6371 * 1000; // metres
            double phi1 = sLocation.Lattitude * Math.PI / 180; // φ, λ in radians
            double phi2 = dLocation.Lattitude * Math.PI / 180;
            double deltaPhi = (dLocation.Lattitude - sLocation.Lattitude) * Math.PI / 180;
            double deltaLambda = (dLocation.Longitude - sLocation.Longitude) * Math.PI / 180;

            double a = Math.Sin(deltaPhi / 2) * Math.Sin(deltaPhi / 2) +
                       Math.Cos(phi1) * Math.Cos(phi2) *
                       Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c / 1000; // in kilometres
            return d;
        }
    }
}
