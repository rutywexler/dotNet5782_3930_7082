using PL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace PL
{
    public class DroneInParcel:INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private double batteryStatus;
        public double BatteryStatus
        {
            get { return batteryStatus; }
            set
            {
                batteryStatus = value;
                OnPropertyChanged(nameof(BatteryStatus));
            }
        }

        private Location current = new Location();
        public Location CurrentLocation
        {
            get { return current; }
            set
            {
                current = value;
                OnPropertyChanged(nameof(CurrentLocation));
            }
        }

        public override string ToString() => this.ToStringProperties();

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
