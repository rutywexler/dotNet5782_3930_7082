using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PL.Enums;

namespace PL
{
    public class DroneToAdd : INotifyPropertyChanged
    {

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

        private WeightCategories? weight;
        public WeightCategories? Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }



        private int? stationId;
        public int? StationId
        {
            get { return StationId; }
            set
            {
                stationId = value;
                OnPropertyChanged(nameof(StationId));
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


