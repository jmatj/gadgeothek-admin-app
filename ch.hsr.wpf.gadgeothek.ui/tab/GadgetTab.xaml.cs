using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;
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
        private LibraryAdminService libraryAdminService;
        public ObservableCollection<Gadget> Gadgets { get; set; }

        public GadgetTab()
        {
            InitializeComponent();

            LoadData();
            GadgetList.SelectedIndex = 0;

            DataContext = this;
        }

        private void LoadData()
        {
            libraryAdminService = new LibraryAdminService(ConfigurationManager.AppSettings["server"]);

            LoadGadgets();
        }

        private void LoadGadgets()
        {
            Gadgets = new ObservableCollection<Gadget>();
            foreach (Gadget gadget in libraryAdminService.GetAllGadgets())
            {
                Gadgets.Add(gadget);
            }
        }

        private void UpdateGadget_Click(object sender, RoutedEventArgs e)
        {
            libraryAdminService.UpdateGadget(GadgetList.SelectedItem as Gadget);
        }

        private void DeleteGadget_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Soll das Gadget gelöscht werden?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Gadget gadgetToRemove = GadgetList.SelectedItem as Gadget;
                libraryAdminService.DeleteGadget(gadgetToRemove);
                Gadgets.Remove(gadgetToRemove);
            }
        }
    }
}
