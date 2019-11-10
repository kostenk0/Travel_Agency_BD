﻿using HappyTravel.Tools.Navigation;
using HappyTravel.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HappyTravel.Views
{
    /// <summary>
    /// Interaction logic for ContractView.xaml
    /// </summary>
    public partial class ContractView : UserControl, INavigatable
    {
        public ContractView()
        {
            InitializeComponent();
            DataContext = new ContractViewModel();
        }
    }
}
