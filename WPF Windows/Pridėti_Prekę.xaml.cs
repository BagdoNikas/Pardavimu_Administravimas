using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Administravimas.WPF_Windows
{
    /// <summary>
    /// Interaction logic for Pasalinti.xaml
    /// </summary>
    public partial class Prideti_preke : Window
    {
        string senasid;
        public Prideti_preke()
        {
            InitializeComponent();
            GenerateID();
        }

        /// <summary>
        /// Sukuria Naują ID numerį
        /// </summary>
        private void GenerateID()
        {
            senasid = Metodai.id_prekes;
            string id = "P" + (int.Parse(senasid.Substring(2)) + 1).ToString("D4");
            Metodai.id_prekes = id;
            pid.Text = id;
        }

        /// <summary>
        /// Įjungia mygtuką pridęti kai visi laukai užpyldomi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableButton(object sender, TextChangedEventArgs e)
        {
            if (pid.IsLoaded)
            {
                if (pid.Text.Length > 0 && pavadinimas.Text.Length > 0 && kaina.Text.Length > 0)
                    Add.IsEnabled = true;
                else
                    Add.IsEnabled = false;
            }
        }

        /// <summary>
        /// Sukuria naują elementą ir pridedą į listą
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            Metodai.Nauja_Prek(pid.Text, pavadinimas.Text, double.Parse(kaina.Text));
            this.Close();
            }
            catch
            {
                MessageBox.Show("Blogai įvesta kaina");
            }
        }

        /// <summary>
        /// Uždaro tik šį langą
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
