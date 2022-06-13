using FahrzeugDatenbank;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FahrzeugeWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string connectionString = this.GetConnectionString();
            var repository = new FahrzeugRepository(connectionString);
            var modell = new FahrzeugeModell(repository);

            var viewModel = new MainWindowViewModel(modell);

            this.MainWindow = new MainWindow();
            this.MainWindow.DataContext = viewModel;
            this.MainWindow.Show();
        }

        private string GetConnectionString()
        {
            return "Server=localhost;User ID=root;Password=admin;Database=FahrzeugDB";
        }
    }
}
