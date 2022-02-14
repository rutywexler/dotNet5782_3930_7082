using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static PL.Enums;

namespace PL
{
    public class DroneForList:ILocate,INotifyPropertyChanged
    {
        private int id;
        public int Id { 
            get{ return id; } 
            set{ id = value;
                OnPropertyChanged(nameof(Id)); } }

        private string model;
        public string Model
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        private WeightCategories weight;
        public WeightCategories Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        private int battery;
        public int Battery
        {
            get { return battery; }
            set
            {
                battery = value;
                OnPropertyChanged(nameof(Battery));
            }
        }

        private DroneStatuses status;
        public DroneStatuses Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private Location location = new Location();
        public Location Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        private int deliveryId;
        public int DeliveryId
        {
            get { return deliveryId; }
            set
            {
                deliveryId = value;
                OnPropertyChanged(nameof(DeliveryId));
            }
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
