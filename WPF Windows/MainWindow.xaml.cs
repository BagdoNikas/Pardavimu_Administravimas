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
        public static MainWindow main = null;

        public MainWindow()
        {
            main = this;
            InitializeComponent();
            Metodai.Skaitymas();
            Update_Data_Grid();
        }

        public void Update_Data_Grid()
        {
            for (int i = 0; i < Metodai.Užsakymai.Count; i++)
            {
                DuomenuGrid.Items.Clear();
            }
            for (int i = 0; i < Metodai.Užsakymai.Count; i++)
            {
                var užsakymas = Metodai.Užsakymai[i];
                DuomenuGrid.Items.Add(užsakymas);
            }
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

        private void Pasalinti_Click(object sender, RoutedEventArgs e)
        {
            var PasalintiWindow = new Salinimas();
            PasalintiWindow.Show();
        }

        private void PridetiPreke_Click(object sender, RoutedEventArgs e)
        {
            var PridetiprekeWindow = new Prideti_preke();
            PridetiprekeWindow.Show();
        }
    }
}
