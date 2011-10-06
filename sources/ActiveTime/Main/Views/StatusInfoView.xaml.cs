﻿using System;
using System.Windows.Controls;
using DustInTheWind.ActiveTime.Main.ViewModels;

namespace DustInTheWind.ActiveTime.Main.Views
{
    /// <summary>
    /// Interaction logic for StatusView.xaml
    /// </summary>
    public partial class StatusInfoView : UserControl
    {
        public StatusInfoView(StatusInfoViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");

            InitializeComponent();

            Loaded += (s, e) => DataContext = viewModel;
        }
    }
}
