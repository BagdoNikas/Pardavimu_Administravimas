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
using System.Windows.Shapes;

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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Metodai.Nauja_Prek(pid.Text, pavadinimas.Text, double.Parse(kaina.Text));
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
