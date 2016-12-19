using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;
using ch.hsr.wpf.gadgeothek.ui.command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace ch.hsr.wpf.gadgeothek.ui.viewmodel
{
    class LoanVM : BindableBase
    {
        private LibraryAdminService libraryAdminService;

        private ObservableCollection<Loan> _loans;
        public ObservableCollection<Loan> Loans
        {
            get { return _loans; }
            set { SetProperty(ref _loans, value, nameof(Loans)); }
        }

        private Loan _selectedLoan;
        public Loan SelectedLoan
        {
            get { return _selectedLoan; }
            set { SetProperty(ref _selectedLoan, value, nameof(SelectedLoan)); }
        }
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value, nameof(IsRefreshing)); }
        }
        public ICommand RefreshCommand { get; set; }

        public LoanVM()
        {
            RefreshCommand = new RelayCommand(() => Refresh(), () => true);
            Loans = new ObservableCollection<Loan>();
            LoadData();
        }

        private void LoadData()
        {
            libraryAdminService = new LibraryAdminService(ConfigurationManager.AppSettings["server"]);
            LoadLoans(Loans);
        }

        private void LoadLoans(Collection<Loan> targetCollection)
        {
            foreach (Loan loan in libraryAdminService.GetAllLoans())
            {
                targetCollection.Add(loan);
            }
        }
        private void Refresh()
        {
            IsRefreshing = true;
            int selectedIndex = Loans.IndexOf(SelectedLoan);
            Task.Run(() =>
            {
                var refreshedLoans = new ObservableCollection<Loan>();
                LoadLoans(refreshedLoans);
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    Loans = refreshedLoans;
                    SelectedLoan = Loans.ElementAt(selectedIndex);
                    IsRefreshing = false;
                });
            });
        }
    }
}
