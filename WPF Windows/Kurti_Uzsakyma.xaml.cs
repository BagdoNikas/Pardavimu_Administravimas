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
    /// Interaction logic for Kurti_Uzsakyma.xaml
    /// </summary>
    public partial class Kurti_Uzsakyma : Window
    {
        string senasid;
        public Kurti_Uzsakyma()
        {
            InitializeComponent();

            GenerateID();
        }

        private void Kurti_Click(object sender, RoutedEventArgs e)
        {
            string pidkiekis = new TextRange(Id_Kiekis_RichText.Document.ContentStart, Id_Kiekis_RichText.Document.ContentEnd).Text;
            pidkiekis = pidkiekis.Remove(pidkiekis.Length - 2);
            DateTime data = DateTime.Parse(Data_text.Text);
            Metodai.Naujas_Užsakymas(ID_text.Text, Klientas_text.Text, double.Parse(Suma_text.Text),
                Pardavejas_text.Text, data, pidkiekis);
            //Naudojant M.Užsakymai ir M.Detales užpildyti gridą
        }

        /// <summary>
        /// Išjungia tik Kurti užsakymą langą
        /// </summary>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Metodai.id_uzsakymo = senasid;
            this.Close();
        }

        /// <summary>
        /// jeigu visi texbox yra užpilditi tai Kurti mygtukas įjungiamas, jei neužpilditi tai iįjungiamas
        /// </summary>
        private void EnableButton(object sender, TextChangedEventArgs e)
        {
            if (ID_text.IsLoaded)
            {
                string richtext = new TextRange(Id_Kiekis_RichText.Document.ContentStart, Id_Kiekis_RichText.Document.ContentEnd).Text;
                richtext = richtext.Remove(richtext.Length - 2);
                if (ID_text.Text.Length > 0 && Klientas_text.Text.Length > 0 && Suma_text.Text.Length > 0 && Pardavejas_text.Text.Length > 0 && Data_text.Text.Length > 0 && richtext.Length > 2)
                    Kurti.IsEnabled = true;
                else
                    Kurti.IsEnabled = false;

            }
        }

        /// <summary>
        /// Sukuria Naują ID numerį
        /// </summary>
        private void GenerateID()
        {
            senasid = Metodai.id_uzsakymo;
            string id = "U_" + (int.Parse(senasid.Substring(2)) + 1).ToString("D4");
            Metodai.id_darbuotojo = id;
            ID_text.Text = id;
        }
    }
}
