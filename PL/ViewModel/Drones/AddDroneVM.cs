using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static PL.Enums;

namespace PL
{
    public class AddDroneVM
    {
        BlApi.IBL bl;
        public int StationId { get; set; }
        public Array Weight { get; set; }
        public DroneForList drone { get; set; }
        public IEnumerable<int> StationsId { get; set; }
        public RelayCommand AddDroneCommand { get; set; }
 
        public AddDroneVM()
        {
            bl = BlApi.BlFactory.GetBL();
            drone = new();
            AddDroneCommand = new(AddDrone, null)/*checkValid.CheckValidAddCustomer)*/;
            Weight = Enum.GetValues(typeof(WeightCategories));
            StationsId= bl.GetStaionsWithEmptyChargeSlots().Select(station => station.IdStation);
        }

        private void AddDrone(object parameter)
        {
            try
            {
                var droneBO = DroneConverter.ConvertBackDrone(drone);
                bl.AddDrone(droneBO.DroneId,droneBO.DroneWeight,droneBO.ModelDrone, StationId);
                MessageBox.Show("Success to add drone:)");

            }
            catch (KeyNotFoundException ex)
            {

                MessageBox.Show($"Didnt succeed to add the drone:( Enter details Again,{ex.Message}");
            }
        }
    }
}
