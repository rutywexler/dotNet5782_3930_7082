using PL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static PL.Enums;

namespace PL
{
    public class ParcelForList :INotifyPropertyChanged
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
   
        private string senderName;
        public string SenderName
        {
            get { return senderName; }
            set
            {
                senderName = value;
                OnPropertyChanged(nameof(SenderName));
            }
        }

        private string targetName;
        public string TargetName
        {
            get { return targetName; }
            set
            {
                targetName = value;
                OnPropertyChanged(nameof(TargetName));
            }
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
 
        private DeliveryStatus status;
        public DeliveryStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
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