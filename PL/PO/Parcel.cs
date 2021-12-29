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
    public class Parcel:INotifyPropertyChanged
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

        private CustomerInParcel sender;
        public CustomerInParcel Sender {
            get { return sender; }
            set { sender = value; OnPropertyChanged(nameof(Sender)); } 
        }

        private CustomerInParcel target;
        public CustomerInParcel Target
        {
            get { return target; }
            set { target = value; OnPropertyChanged(nameof(Target)); }
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

        private DroneInParcel drone1;
        public DroneInParcel Drone1 {
            get { return drone1; }
            set { drone1 = value; OnPropertyChanged(nameof(Drone1)); }
        }

        private DateTime? created;
        public DateTime? Created {
            get { return created; }
            set { created = value; OnPropertyChanged(nameof(Created)); } 
        }

        private DateTime? scheduled;
        public DateTime? Scheduled
        {
            get { return scheduled; }
            set { scheduled = value; OnPropertyChanged(nameof(Scheduled)); }
        }

        private DateTime? pickedUp;
        public DateTime? PickedUp
        {
            get { return pickedUp; }
            set { pickedUp = value; OnPropertyChanged(nameof(PickedUp)); }
        }

        private DateTime? delivered;
        public DateTime? Delivered
        {
            get { return delivered; }
            set { delivered = value; OnPropertyChanged(nameof(Delivered)); }
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
