using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public BlApi.IBL bl { get; set; }
        public MainWindowViewModel()
        {
            bl = BlApi.BlFactory.GetBL();
        }
        private object currentView;

        public object CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentView)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
