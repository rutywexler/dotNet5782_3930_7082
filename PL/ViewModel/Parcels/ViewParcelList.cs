using PL.Model;
using PL.UsingBl;
using PL.View.Parcel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PL.ViewModel.Parcels
{
    public class ViewParcelList : DependencyObject
    {
        public ListCollectionView ViewParcels
        {
            get { return (ListCollectionView)GetValue(ViewParcelsProperty); }
            set { SetValue(ViewParcelsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewParcelsProperty =
            DependencyProperty.Register("ViewParcels", typeof(ListCollectionView), typeof(ViewParcelList), new PropertyMetadata(null));


        //public IEnumerable<ParcelForList> ViewParcels { get; set; }
        BlApi.IBL bl;
         Enums.DeliveryStatus? selectedFilterByStatus;

        public RelayCommand ViewParcelsList { get; set; }
        public RelayCommand OpenAddParcelWindow { get; set; }
        public RelayCommand OpenViewParcelsWindowCommand { get; set; }
        public RelayCommand GroupingParcelList { get; set; }
        public ObservableCollection<string> ComboboxItems { get; set; }
        public ViewParcelList()
        {
            bl = BlApi.BlFactory.GetBL();
            InitializeList();
            OpenAddParcelWindow = new(OpenAddWindow, null);
            OpenViewParcelsWindowCommand = new(OpenParcelView);
            GroupingParcelList = new(Grouping, null);
            ComboboxItems = new ObservableCollection<string>(typeof(ParcelForList).GetProperties().Where(prop => prop.PropertyType.IsValueType || prop.PropertyType == typeof(string)).Select(prop => prop.Name));
            StatusList = Enum.GetValues<Enums.DeliveryStatus>();
        }

        private bool ParcelFilter(object obj)
        {
            if(obj is ParcelForList parcel)
            {
                if (!SelectedFilterByStatus.HasValue || parcel.Status == SelectedFilterByStatus.Value)
                    return true;
            }
            return false;
        }

        private void InitializeList()
        {
            ViewParcels = new ListCollectionView(GetParcels().ToList());
            ViewParcels.Filter = ParcelFilter;
        }
        public void OpenParcelView(object param)
        {
            var parcel = param as ParcelForList;
            new ViewParcel(parcel).ShowDialog();
            InitializeList();
        }
        public IEnumerable<ParcelForList> GetParcels()
        {
            return bl.GetParcels().Select(parcel => ParcelConverter.ConvertParcelForListBoToPo(parcel)).ToList();
        }

        public void OpenAddWindow(object param)
        {
            new AddParcels().ShowDialog();
            InitializeList();
        }

        public void Grouping(object param)
        {
            for (int i = 0; i < ViewParcels.GroupDescriptions.Count; i++)
            {
                ViewParcels.GroupDescriptions.RemoveAt(i);
            }
            ViewParcels.GroupDescriptions.Add(new PropertyGroupDescription(param.ToString()));
        }

        public IEnumerable<Enums.DeliveryStatus> StatusList { get; set; }
        public Enums.DeliveryStatus? SelectedFilterByStatus
        {
            get => selectedFilterByStatus;
            set
            {
                selectedFilterByStatus = value;
                ViewParcels.Refresh();
            }
        }
    }

}
