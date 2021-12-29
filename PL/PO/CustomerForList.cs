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
    public class CustomerForList : INotifyPropertyChanged
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

        private int deliveredPackages;
        public int DeliveredPackages
        {
            get { return deliveredPackages; }
            set
            {
                deliveredPackages = value;
                OnPropertyChanged(nameof(DeliveredPackages));
            }
        }


        private int sendedPackages;
        public int SendedPackages
        {
            get { return sendedPackages; }
            set
            {
                sendedPackages = value;
                OnPropertyChanged(nameof(SendedPackages));
            }
        }

        private int acceptedPackages;
        public int AcceptedPackages
        {
            get { return acceptedPackages; }
            set
            {
                acceptedPackages = value;
                OnPropertyChanged(nameof(AcceptedPackages));
            }
        }

        private int packagesInWay;
        public int PackagesInWay
        {
            get { return packagesInWay; }
            set
            {
                packagesInWay = value;
                OnPropertyChanged(nameof(PackagesInWay));
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