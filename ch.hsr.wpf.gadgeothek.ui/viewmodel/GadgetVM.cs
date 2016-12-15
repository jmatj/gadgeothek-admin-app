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
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ch.hsr.wpf.gadgeothek.ui.viewmodel
{
    class GadgetVM : BindableBase
    {
        private LibraryAdminService libraryAdminService;
        public ObservableCollection<Gadget> Gadgets { get; set; }
        public bool _gadgetNotSaved;
        public bool GadgetNotSaved
        {
            get { return _gadgetNotSaved; }
            set { SetProperty(ref _gadgetNotSaved, value, nameof(GadgetNotSaved)); }
        }
        private Gadget _selectedGadget;
        public Gadget SelectedGadget
        {
            get { return _selectedGadget; }
            set { SetProperty(ref _selectedGadget, value, nameof(SelectedGadget)); }
        }
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value, nameof(IsRefreshing)); }
        }

        public ICommand CreateNewGadgetCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshCommand { get; set; }


        public GadgetVM() {
            LoadData();

            CreateNewGadgetCommand = new RelayCommand(() => CreateNewGadget(), () => true);
            SaveCommand = new RelayCommand(() => Save(), () => true);
            UpdateCommand = new RelayCommand(() => Update(), () => true);
            DeleteCommand = new RelayCommand(() => Delete(), () => true);
            RefreshCommand = new RelayCommand(() => Refresh(), () => true);

            SelectedGadget = Gadgets.ElementAt(0);
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

        private void CreateNewGadget()
        {
            GadgetNotSaved = true;
            Gadget newGadget = new Gadget("");
            Gadgets.Add(newGadget);
            SelectedGadget = newGadget;
        }

        private void Save()
        {
            libraryAdminService.AddGadget(SelectedGadget);
            GadgetNotSaved = false;
        }

        private void Update()
        {
            libraryAdminService.UpdateGadget(SelectedGadget);
        }

        private void Delete()
        {
            if (MessageBox.Show(Properties.Resources.DELETE_CONFIRMATION_TEXT, Properties.Resources.DELETE_CONFIRMATION_TITLE, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                libraryAdminService.DeleteGadget(SelectedGadget);
                Gadgets.Remove(SelectedGadget);
                SelectedGadget = Gadgets.ElementAt(0);
            }
        }

        private void Refresh()
        {
            IsRefreshing = true;
            Task.Run(() =>
            {
                LoadGadgets();
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    IsRefreshing = false;
                });
            });
        }
    }   
}
