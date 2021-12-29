using System.Collections.Generic;
using System.ComponentModel;


namespace PL.Model
{
    public class BaseStation : ILocate, INotifyPropertyChanged
    {
        private int id;
        public int Id { 
            get { return id; }
            set { id = value; OnPropertyChanged(nameof(Id)); } 
        }

        private string name;
        public string Name {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); } 
        }

        private Location location = new Location();
        public Location Location {
            get { return location; }
            set { location = value; OnPropertyChanged(nameof(Location)); } 
        }

        private int availableChargeSlots;
        public int AvailableChargeSlots {
            get { return availableChargeSlots; }
            set { availableChargeSlots = value; OnPropertyChanged(nameof(AvailableChargeSlots)); } 
        }

        private List<DroneInCharging> dronesInCharching = new List<DroneInCharging>();
        public List<DroneInCharging> DronesInCharching {
            get { return dronesInCharching; }
            set { dronesInCharching = value; OnPropertyChanged(nameof(DronesInCharching)); } 
        }

        #region INotifyPropertyChanged Members  

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}

