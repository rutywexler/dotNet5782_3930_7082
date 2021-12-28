using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace PL.Model
{
    public class Customer : ILocate, INotifyPropertyChanged
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

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        private Location location = new Location();
        public Location Location
        {
            get { return location; }
            set { location = value; OnPropertyChanged(nameof(Location)); }
        }

        private List<ParcelToCustomer> fromCustomer = new List<ParcelToCustomer>();
        public List<ParcelToCustomer>  FromCustomer {
            get { return fromCustomer; }
            set { fromCustomer = value; OnPropertyChanged(nameof(FromCustomer)); } 
        }

        private List<ParcelToCustomer> toCustomer = new List<ParcelToCustomer>();
        public List<ParcelToCustomer> ToCustomer
        {
            get { return toCustomer; }
            set { toCustomer = value; OnPropertyChanged(nameof(ToCustomer)); }
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

