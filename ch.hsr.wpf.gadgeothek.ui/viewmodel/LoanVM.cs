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

        public ObservableCollection<Loan> Loans { get; set; }

        public ICommand RefreshCommand { get; set; }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value, nameof(IsRefreshing)); }
        }
        public LoanVM()
        {
            RefreshCommand = new RelayCommand(() => Refresh(), () => true);

            LoadData();
        }

        private void LoadData()
        {
            libraryAdminService = new LibraryAdminService(ConfigurationManager.AppSettings["server"]);
            LoadLoans();
        }

        private void LoadLoans()
        {
            Loans = new ObservableCollection<Loan>();
            foreach (Loan loan in libraryAdminService.GetAllLoans())
            {
                Loans.Add(loan);
            }
        }
        private void Refresh()
        {
            IsRefreshing = true;
            Task.Run(() =>
            {
                LoadLoans();
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    IsRefreshing = false;
                });
            });
        }
    }
}
