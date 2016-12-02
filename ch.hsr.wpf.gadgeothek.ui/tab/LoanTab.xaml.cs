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
    /// Interaction logic for LoanTab.xaml
    /// </summary>
    public partial class LoanTab : UserControl
    {
        private LibraryAdminService libraryAdminService;
        public ObservableCollection<Loan> Loans { get; set; }
        public ObservableCollection<Reservation> Reservations { get; set; }

        public LoanTab()
        {
            InitializeComponent();
            LoadData();

            DataContext = this;
        }

        private void LoadData()
        {
            libraryAdminService = new LibraryAdminService(ConfigurationManager.AppSettings["server"]);

            LoadLoans();
            LoadReservations();
        }

        private void LoadLoans()
        {
            Loans = new ObservableCollection<Loan>();
            foreach (Loan loan in libraryAdminService.GetAllLoans())
            {
                Loans.Add(loan);
            }
        }

        public void LoadReservations()
        {
            Reservations = new ObservableCollection<Reservation>();
            foreach (Reservation reservation in libraryAdminService.GetAllReservations())
            {
                Reservations.Add(reservation);
            }
        }
    }
}
