using Dal;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.CompilerServices;


namespace Dal
{
    public partial class DalXml
    {
        private readonly string parcelsPath = "Parcels.xml";
        private readonly string ConfigPath = @"XmlConfig.xml";
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
            List<Parcel> parcels = XMLTools.LoadListFromXmlSerializer<Parcel>(parcelsPath);
            //if (!Dal.DalObject.ExistsIDCheck(GetCustomers(), SenderId))
            //    throw new KeyNotFoundException("Sender not exist");
            //if (!Dal.DalObject.ExistsIDCheck(GetCustomers(), TargetId))
            //    throw new KeyNotFoundException("Target not exist");
            Parcel newParcel = new();
            XElement config = XMLTools.LoadListFromXmlElement(ConfigPath);
            XElement parcelId = config.Elements().Single(elem => elem.Name.ToString().Contains("Parcel"));
            newParcel.Id = id == 0 ? int.Parse(parcelId.Value) + 1 : id;
            config.SetElementValue(parcelId.Name, newParcel.Id);
            XMLTools.SaveListToXmlElement(config, ConfigPath);
            newParcel.SenderId = SenderId;
            newParcel.TargetId = TargetId;
            newParcel.Weight = Weigth;
            newParcel.Priority = Priority;
            newParcel.Requested = requested == default ? DateTime.Now : requested;
            newParcel.Scheduled = sceduled;
            newParcel.PickedUp = pickedUp;
            newParcel.Delivered = delivered;
            newParcel.DroneId = droneId;
            parcels.Add(newParcel);
            XMLTools.SaveListToXmlSerializer(parcels, parcelsPath);
        }


        /// <summary>
        /// Find a parcel that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested parcel</param>
        /// <returns>A parcel for display</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int id)
        {
            try { 
            Parcel parcel = XMLTools.LoadListFromXmlSerializer<Parcel>(parcelsPath).FirstOrDefault(item => item.Id == id);
                if (parcel.Equals(default(Parcel)))
                    throw new KeyNotFoundException("There isnt suitable parcel in the data!");
                return parcel;
            }
            catch
            {
                throw new KeyNotFoundException();
            }
            
        }

        /// <summary>
        /// Prepares the list of Parcels for display
        /// </summary>
        /// <returns>A list of parcel</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels()
        {
            return XMLTools.LoadListFromXmlSerializer<Parcel>(parcelsPath);
        }

        /// <summary>
        /// the function return parcels that dont assignment
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<Parcel> GetUnAssignmentParcels()
        //{
        //    return DataSource.Parcels.Where(parcel => parcel.DroneId == 0);
        // }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicate = null) =>
           predicate == null ?
               XMLTools.LoadListFromXmlSerializer<Parcel>(parcelsPath) :
               XMLTools.LoadListFromXmlSerializer<Parcel>(parcelsPath).Where(p => predicate(p));


        /// <summary>
        /// Assign parcel to drone 
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        /// <param name="droneId">droneId</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AssignParcelToDrone(int parcelId, int droneId)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXmlSerializer<Parcel>(parcelsPath);
            Parcel parcel = GetParcel(parcelId);
            parcels.Remove(parcel);
            parcel.DroneId = droneId;
            parcel.Scheduled = DateTime.Now;
            parcels.Add(parcel);
            XMLTools.SaveListToXmlSerializer(parcels, parcelsPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(Parcel parcel)
        {

        }


        /// <summary>
        /// Removing a Parcel from the list
        /// </summary>
        /// <param name="station"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveParcel(int id)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXmlSerializer<Parcel>(parcelsPath);
            Parcel parcel = parcels.FirstOrDefault(parcel => parcel.Id == id);
            parcels.Remove(parcel);
            parcel.IsDeleted = true;
            parcels.Add(parcel);
            XMLTools.SaveListToXmlSerializer(parcels, parcelsPath);
        }


    }
}
