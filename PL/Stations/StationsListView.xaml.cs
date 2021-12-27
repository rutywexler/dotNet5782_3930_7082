using System.Windows;

namespace PL.Stations
{
    /// <summary>
    /// Interaction logic for StationsListView.xaml
    /// </summary>
    public partial class StationsListView : Window
    {
        public BlApi.IBL bl { get; set; }

        public StationsListView()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBL();
           DataContext = bl.GetStations();
        }

    }
}
