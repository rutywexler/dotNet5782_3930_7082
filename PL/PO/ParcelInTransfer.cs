using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static PL.Model.Enums;


namespace PL.Model
{
    public class ParcelInTransfer : ILocate, INotifyPropertyChanged
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

        private bool status;
        public bool Status { 
            get { return status; }
            set { status = value; OnPropertyChanged(nameof(Status)); } 
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

        private Priorities priority;
        public Priorities Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }

        private CustomerInParcel sender = new CustomerInParcel();
        public CustomerInParcel Sender {
            get { return sender; }
            set { sender = value; OnPropertyChanged(nameof(Sender)); }
        }

        private CustomerInParcel target = new CustomerInParcel();
        public CustomerInParcel Target
        {
            get { return target; }
            set { target = value; OnPropertyChanged(nameof(Target)); }
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

        private Location destination = new Location();
        public Location Destination
        {
            get { return destination; }
            set
            {
                destination = value;
                OnPropertyChanged(nameof(Destination));
            }
        }

        private double transportDistance;
        public double TransportDistance {
            get { return transportDistance; }
            set { transportDistance = value; OnPropertyChanged(nameof(TransportDistance)); } 
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
