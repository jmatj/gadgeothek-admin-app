using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.hsr.wpf.gadgeothek.ui.viewmodel
{
    class LoanVM : BindableBase
    {
        private LibraryAdminService libraryAdminService;
        public ObservableCollection<Loan> Loans { get; set; }

        public LoanVM()
        {
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
    }
}
