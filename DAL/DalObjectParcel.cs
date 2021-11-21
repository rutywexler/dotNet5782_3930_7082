using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DalObject.DataSource;

namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// AddParcel is a method in the DalObject class.
        /// the method adds a new parcel
        /// </summary>
        public void InsertParcel(Parcel parcel) => Parcels.Add(parcel);

        /// <summary>
        /// DisplayParcel is a method in the DalObject class.
        /// the method allows parcel view
        /// </summary>
        public void DisplayParcel()
        {
            Console.WriteLine("Enter parcel id:");
            int input;
            ValidRange(0, Parcels.Count, out input);
            Console.WriteLine(Parcels[input - 1]);
        }

        /// <summary>
        /// ViewListParcels is a method in the DalObject class.
        /// the method displays the list of parcels
        /// </summary>
        public void ViewListParcels()
        {
            foreach (Parcel item in Parcels)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// UpdateScheduled is a method in the DalObject class.
        /// the method assigns a package to the drone
        /// </summary>
        /// <param name="id">int value</param>
        public void UpdateScheduled(int id)
        {
            checkValid(id, 0, Parcels.Count);
            foreach (Drone drone in Drones)
            {
                if ((drone.MaxWeight >= Parcels[Parcels.Count].Weight) && (drone.Battery >= 30) && drone.Status == 0)
                {
                    Parcel parcelTemp = Parcels[id];
                    parcelTemp.DroneId = drone.Id;
                    parcelTemp.Scheduled = DateTime.Now;
                    Parcels[id] = parcelTemp;
                    return;
                }
            }
        }

        /// <summary>
        /// ViewListPendingParcels is a method in the DalObject class.
        /// the method displays a list of packages that have not yet been assigned to the drone
        /// </summary>
        public void ViewListPendingParcels()
        {
            foreach (Parcel parcel in Parcels)
            {
                if (parcel.DroneId == 0)
                {
                    Console.WriteLine(parcel);
                }
            }
        }

        /// <summary>
        /// Find a parcel that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested parcel</param>
        /// <returns>A parcel for display</returns>
        public Parcel GetParcel(int id)
        {
            return Parcels.First(item => item.Id == id);
        }

        /// <summary>
        /// Prepares the list of Parcels for display
        /// </summary>
        /// <returns>A list of parcel</returns>
        public IEnumerable<Parcel> GetParcels() => Parcels;


    }
}
