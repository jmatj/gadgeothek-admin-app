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
using System.Windows.Data;
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
            SelectedLoan = Loans.ElementAt(0);
        }

        private void LoadData()
        {
            libraryAdminService = new LibraryAdminService(ConfigurationManager.AppSettings["server"]);
            LoadLoans();
        }

        private void LoadLoans()
        {
            foreach (Loan loan in libraryAdminService.GetAllLoans())
            {
                Loans.Add(loan);
            }
        }
        private void Refresh()
        {
            int selectedIndex = Loans.IndexOf(SelectedLoan);
            Loans.Clear();
            IsRefreshing = true;
            BindingOperations.EnableCollectionSynchronization(_loans, new object());
            Task.Run(() =>
            {
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    LoadLoans();
                    SelectedLoan = Loans.ElementAt(selectedIndex);
                    IsRefreshing = false;
                });
            });
        }
    }
}
