using PL.Model.Po;
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
    public class BaseStationForList :INotifyPropertyChanged
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
      
        private int numOfAvailableChargingStations;
        public int NumOfAvailableChargingStations
        {
            get { return numOfAvailableChargingStations; }
            set
            {
                numOfAvailableChargingStations = value;
                OnPropertyChanged(nameof(NumOfAvailableChargingStations));
            }
        }

      
        private int numOfBusyChargingStations;
        public int NumOfBusyChargingStations
        {
            get { return numOfBusyChargingStations; }
            set
            {
                numOfBusyChargingStations = value;
                OnPropertyChanged(nameof(numOfBusyChargingStations));
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

