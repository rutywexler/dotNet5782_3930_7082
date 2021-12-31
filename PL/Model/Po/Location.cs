using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PL.Model
{
    public class Location: INotifyPropertyChanged
    {
        private double? longitude;
        public double? Longitude { 
            get { return longitude; } 
            set { longitude = value; 
                OnPropertyChanged(nameof(Longitude)); } }

        private double? latitude { get; set; }
        public double? Latitude {
            get { return latitude; }
            set { latitude = value;
                OnPropertyChanged(nameof(Latitude));} }

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
