using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;
using ch.hsr.wpf.gadgeothek.ui.viewmodel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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

namespace ch.hsr.wpf.gadgeothek.ui.tab
{
    /// <summary>
    /// Interaction logic for GadgetTab.xaml
    /// </summary>
    public partial class GadgetTab : UserControl
    {
        public GadgetTab()
        {
            InitializeComponent();

            DataContext = new GadgetVM();
            GadgetList.SelectedIndex = 0;
        }       
    }
}