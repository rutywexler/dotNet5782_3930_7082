using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dal.DataSource;
using System.Runtime.CompilerServices;

namespace Dal
{
    public partial class DalObject
    {
        /// <summary>
        /// Gets parameters and create new parcel 
        /// </summary>
        /// <param name="SenderId"> Id of sener</param>
        /// <param name="TargetId"> Id of target</param>
        /// <param name="Weigth"> The weigth of parcel (light- 0,medium - 1,heavy - 2)</param>
        /// <param name="Priority"> The priority of send the parcel (regular - 0,fast - 1,emergency - 2)</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority, int id = 0, int droneId = 0, DateTime requested = default, DateTime sceduled = default, DateTime pickedUp = default, DateTime? delivered = default)
        {
            if (!ExistsIDCheck(GetCustomers(), SenderId))
                throw new KeyNotFoundException("Sender not exist");
            if (!ExistsIDCheck(GetCustomers(), TargetId))
                throw new KeyNotFoundException("Target not exist");
            Parcel newParcel = new();
            newParcel.Id = id == 0 ? ++Config.IdParcel : id;
            newParcel.SenderId = SenderId;
            newParcel.TargetId = TargetId;
            newParcel.Weight = Weigth;
            newParcel.Priority = Priority;
            newParcel.Requested = requested == default ? DateTime.Now : requested;
            newParcel.Scheduled = sceduled;
            newParcel.PickedUp = pickedUp;
            newParcel.Delivered = delivered;
            newParcel.DroneId = droneId;
            Parcels.Add(newParcel);
        }


        /// <summary>
        /// Find a parcel that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested parcel</param>
        /// <returns>A parcel for display</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int id)
        {
            Parcel parcel = Parcels.FirstOrDefault(item => item.Id == id);
            if (parcel.Equals(default(Parcel)))
                throw new KeyNotFoundException("There isnt suitable parcel in the data!");
            return parcel;
        }

        /// <summary>
        /// Prepares the list of Parcels for display
        /// </summary>
        /// <returns>A list of parcel</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels()
        {
            return Parcels.Where(parcel => parcel.IsDeleted == false);
        }

        /// <summary>
        /// the function return parcels that dont assignment
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicate)
        {
            return Parcels.Where(parcel => predicate(parcel));
        }

        /// <summary>
        /// Assign parcel to drone 
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        /// <param name="droneId">droneId</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AssignParcelToDrone(int parcelId, int droneId)
        {
            Parcel parcel = GetParcel(parcelId);
            Parcels.Remove(parcel);
            parcel.DroneId = droneId;
            parcel.Scheduled = DateTime.Now;
            Parcels.Add(parcel);
        }


        /// <summary>
        /// Removing a Parcel from the list
        /// </summary>
        /// <param name="station"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveParcel(int id)
        {
            Parcel parcel = Parcels.FirstOrDefault(parcel => parcel.Id == id);
            Parcels.Remove(parcel);
            parcel.IsDeleted = true;
            Parcels.Add(parcel);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveParcelAbsolute(int id)
        {
            Parcel parcel = GetParcel(id);
            Parcels.Remove(parcel);
        }



    }
}
