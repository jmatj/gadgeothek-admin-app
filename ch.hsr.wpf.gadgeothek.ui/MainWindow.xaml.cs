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

namespace ch.hsr.wpf.gadgeothek.ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Gadget> Gadgets { get; set; }
        private LibraryService libraryService;

        public MainWindow()
        {
            InitializeComponent();

            LoadData();

            DataContext = this;
        }

        private void LoadData()
        {
            libraryService = new LibraryService(ConfigurationManager.AppSettings["server"]);
            if (!libraryService.Login("matt.muster@hsr.ch", "12345"))
            {
                Console.WriteLine("Sorry, not authorized");
                return;
            }
            LoadGadgets();
        }

        private void LoadGadgets()
        {
            Gadgets = new ObservableCollection<Gadget>();
            foreach (Gadget gadget in libraryService.GetGadgets())
            {
                Gadgets.Add(gadget);
            }
        }
    }
}
