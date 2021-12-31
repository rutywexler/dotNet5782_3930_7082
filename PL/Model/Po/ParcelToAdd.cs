using PL.Model;
using System.ComponentModel;
using static PL.Model.Enums;

namespace PL.PO
{
    public class ParcelToAdd : INotifyPropertyChanged
    {
       
        private CustomerInParcel sender;
        public CustomerInParcel Sender
        {
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

        private int? id;
        public int? Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
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
