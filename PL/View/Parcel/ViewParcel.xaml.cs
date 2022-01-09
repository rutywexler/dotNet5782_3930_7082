﻿using PL.Model;
using PL.ViewModel.Parcels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.View.Parcel
{
    /// <summary>
    /// Interaction logic for ViewParcel.xaml
    /// </summary>
    public partial class ViewParcel : Window
    {
        public ViewParcel(ParcelForList parcel)
        {
            InitializeComponent();
            DataContext = new ViewParcelVM(parcel);
        }
    }
}
