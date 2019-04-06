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
    /// Interaction logic for Salinimas.xaml
    /// </summary>
    public partial class Salinimas : Window
    {
        public Salinimas()
        {
            InitializeComponent();

            Kategorija_combo.SelectionChanged += SelectionChanged;
            Kategorija_combo.Items.Add("--pasirinkite--");
            Kategorija_combo.Items.Add("Užsakymas");
            Kategorija_combo.Items.Add("Klientas");
            Kategorija_combo.Items.Add("Darbuotojas");
            Kategorija_combo.Items.Add("Prekė");
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Kategorija_combo.Text.Length > 0)
            {
                switch (Kategorija_combo.Text)
                {
                    case "--pasirinkite--":
                        Pavadinimas_combo.Items.Clear();
                        break;
                    case "Užsakymas":
                        Pavadinimas_combo.Items.Clear();
                        for (int i = 0; i < Metodai.Užsakymai.Count; i++)
                        {
                            Pavadinimas_combo.Items.Add(Metodai.Užsakymai[i].ID);
                        }
                        Pavadinimas_combo.Items.Refresh();
                        break;
                    case "Klientas":
                        Pavadinimas_combo.Items.Clear();
                        for (int i = 0; i < Metodai.Klientai.Count; i++)
                        {
                            Pavadinimas_combo.Items.Add(Metodai.Klientai[i].Pavadinimas);
                        }
                        Pavadinimas_combo.Items.Refresh();
                        break;
                    case "Darbuotojas":
                        Pavadinimas_combo.Items.Clear();
                        for (int i = 0; i < Metodai.Pardavėjai.Count; i++)
                        {
                            Pavadinimas_combo.Items.Add(Metodai.Pardavėjai[i].VardasPavardė);
                        }
                        Pavadinimas_combo.Items.Refresh();
                        break;
                    case "Prekė":
                        Pavadinimas_combo.Items.Clear();
                        for (int i = 0; i < Metodai.Prekės.Count; i++)
                        {
                            Pavadinimas_combo.Items.Add(Metodai.Prekės[i].Pavadinimas);
                        }
                        Pavadinimas_combo.Items.Refresh();
                        break;
                    default:
                        Pavadinimas_combo.Items.Clear();
                        break;
                }
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            switch (Kategorija_combo.Text)
            {
                case "Užsakymas":
                    
                    break;
                case "Klientas":
                    
                    break;
                case "Darbuotojas":
                    
                    break;
                case "Prekė":
                    
                    break;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
