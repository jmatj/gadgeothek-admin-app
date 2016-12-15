using AutoMapper;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.ui.viewmodel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ch.hsr.wpf.gadgeothek.ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = new GadgeothekWindow();
            MainWindow.Show();

        }
    }
}
