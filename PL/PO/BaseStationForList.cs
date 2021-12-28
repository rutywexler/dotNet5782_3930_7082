using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static PL.Model.Enum;

namespace PL.Model
{
    class BaseStationForList :INotifyPropertyChanged
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
      
        private int availableChargeSlots;
        public int AvailableChargeSlots
        {
            get { return availableChargeSlots; }
            set
            {
                availableChargeSlots = value;
                OnPropertyChanged(nameof(AvailableChargeSlots));
            }
        }

      
        private int catchChargeSlots;
        public int CatchChargeSlots
        {
            get { return catchChargeSlots; }
            set
            {
                catchChargeSlots = value;
                OnPropertyChanged(nameof(CatchChargeSlots));
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

