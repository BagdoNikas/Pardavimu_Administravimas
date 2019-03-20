using System;
using System.Collections.Generic;
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
using Microsoft.Win32;
using System.IO;
using System.Data;

namespace Administravimas.WPF_Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Metodai.Skaitymas();

        }
        public void FillDataGrid()
        {
            var col = new DataGridTextColumn();
            col.Header = "D";
            col.Binding = new Binding("A");
            DuomenuGrid.Columns.Add(col);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kurti_Užsakymą_Click(object sender, RoutedEventArgs e)
        {
            var KurtiUzakymaWindow = new Kurti_Uzsakyma();
            KurtiUzakymaWindow.Show();
        }

        /// <summary>
        /// Atidaro Formuoti ataskaiktą langą
        /// </summary>
        private void Formuoti_Click(object sender, RoutedEventArgs e)
        {
            var FormuotiAtaskaitawindow = new Formuoti_Ataskaita();
            FormuotiAtaskaitawindow.Show();
        }

        /// <summary>
        /// Išjungia visus atidarytus langus
        /// </summary>
        private void Baigti_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Atidaro Pridėti pardaveją langą
        /// </summary>
        private void PridetiPardaveja_Click(object sender, RoutedEventArgs e)
        {
            var PridetidarbuotojaWindow = new Prideti_Darbuotoja();
            PridetidarbuotojaWindow.Show();
        }

        /// <summary>
        /// Atidaro pridėti klientą langą
        /// </summary>
        private void PridetiKlienta_Click(object sender, RoutedEventArgs e)
        {
            var PridetiKlientaWindow = new Prideti_Klienta();
            PridetiKlientaWindow.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.FillDataGrid();
        }
    }
}
