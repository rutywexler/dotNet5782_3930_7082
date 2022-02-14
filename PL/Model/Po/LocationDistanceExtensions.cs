﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    static class LocationDistanceExtensions
    {
        /// <summary>
        /// The function calculates distance
        /// </summary>
        /// <param name="baseLocation">the first ILocate object</param>
        /// <param name="targetLocation">the second ILocate object</param>
        /// <returns></returns>
        public static double DistanceTo(this ILocate baseLocation, ILocate targetLocation)
        {
            var baseRad = Math.PI * baseLocation.Location.Latitude / 180;
            var targetRad = Math.PI * targetLocation.Location.Latitude / 180;
            var theta = baseLocation.Location.Longitude - targetLocation.Location.Longitude;
            var thetaRad = Math.PI * theta / 180;

            double dist =
                Math.Sin((double)baseRad) * Math.Sin((double)targetRad) + Math.Cos((double)baseRad) *
                Math.Cos((double)targetRad) * Math.Cos((double)thetaRad);
            dist = Math.Acos(dist);

            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            return dist * 1.609344;
        }
    }
}
