﻿using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            Kategorija_combo.Items.Add("Išvalyti duomenis");
        }

        /// <summary>
        /// Pasikeitus vartotojo pasirinkimui atnaujinami Pavadinimas_combobox elementai
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Kategorija_combo.IsLoaded)
            {
                switch (Kategorija_combo.SelectedItem.ToString())
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
                        break;
                    case "Klientas":
                        Pavadinimas_combo.Items.Clear();
                        for (int i = 0; i < Metodai.Klientai.Count; i++)
                        {
                            Pavadinimas_combo.Items.Add(Metodai.Klientai[i].Pavadinimas);
                        }
                        break;
                    case "Darbuotojas":
                        Pavadinimas_combo.Items.Clear();
                        for (int i = 0; i < Metodai.Pardavėjai.Count; i++)
                        {
                            Pavadinimas_combo.Items.Add(Metodai.Pardavėjai[i].VardasPavardė);
                        }
                        break;
                    case "Prekė":
                        Pavadinimas_combo.Items.Clear();
                        for (int i = 0; i < Metodai.Prekės.Count; i++)
                        {
                            Pavadinimas_combo.Items.Add(Metodai.Prekės[i].Pavadinimas);
                        }
                        break;
                    case "Išvalyti duomenis":
                        Pavadinimas_combo.Items.Clear();
                        Pavadinimas_combo.Items.Add("Užsakymai");
                        Pavadinimas_combo.Items.Add("Detalizacija");
                        Pavadinimas_combo.Items.Add("Prekės");
                        Pavadinimas_combo.Items.Add("Klientai");
                        Pavadinimas_combo.Items.Add("Darbuotojai");
                        break;
                    default:
                        Pavadinimas_combo.Items.Clear();
                        break;
                }

                Pavadinimas_combo.Items.Refresh();
                Pavadinimas_combo.SelectedIndex = 0;
                Remove.IsEnabled = true;
            }
        }

        /// <summary>
        /// Ištrina pasirinktus duomenis. Jeigu duomenys naudojami kitoje klasėje vartotojui apie tai praneša ir neleidžia ištrynti.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            bool istrinta = false;
            Užsakymas u = null;
            Detalizacija d = null;
            switch (Kategorija_combo.Text)
            {
                case "Užsakymas":
                    if (MessageBox.Show("Ar tikrai norite pašalinti šiuos duomenis?", "Patvirtinti", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        File.Delete(Metodai.failai[0]);

                        File.Delete(Metodai.failai[1]);
                        for (int i = 0; i < Metodai.Užsakymai.Count; i++)
                        {
                            if (Pavadinimas_combo.Text.Equals(Metodai.Užsakymai[i].ID))
                            {
                                Metodai.Užsakymai.RemoveAt(i);
                                Metodai.Detalės.RemoveAt(i);
                            }
                            if (i != Metodai.Užsakymai.Count)
                            {
                                Metodai.Salinimas(Metodai.Užsakymai[i], 0);
                                Metodai.Salinimas(Metodai.Detalės[i], 1);
                            }
                        }
                        istrinta = true;
                    }
                    break;
                case "Klientas":
                    u = Metodai.Užsakymai.FirstOrDefault(stringToCheck => stringToCheck.Pirkėjas.Equals(Pavadinimas_combo.Text));
                    if (u == null)
                    {
                        File.Delete(Metodai.failai[4]);
                        for (int i = 0; i < Metodai.Klientai.Count; i++)
                        {
                            if (Pavadinimas_combo.Text.Equals(Metodai.Klientai[i].Pavadinimas))
                                Metodai.Klientai.RemoveAt(i);

                            if (i != Metodai.Klientai.Count)
                                Metodai.Salinimas(Metodai.Klientai[i], 4);
                        }
                        istrinta = true;
                    }
                    else
                    {
                        MessageBox.Show("Šis klientas įtrauktas į užsakymus, todėl ištrynus gali dingti reikalinga informacija");
                    }
                    break;
                case "Darbuotojas":
                    u = Metodai.Užsakymai.FirstOrDefault(stringToCheck => stringToCheck.Darbuotojas.Equals(Pavadinimas_combo.Text));
                    if (u == null)
                    {
                        File.Delete(Metodai.failai[3]);
                        for (int i = 0; i < Metodai.Pardavėjai.Count; i++)
                        {
                            if (Pavadinimas_combo.Text.Equals(Metodai.Pardavėjai[i].VardasPavardė))
                                Metodai.Pardavėjai.RemoveAt(i);
                            if (i != Metodai.Pardavėjai.Count)
                                Metodai.Salinimas(Metodai.Pardavėjai[i], 3);

                        }
                        istrinta = true;
                    }
                    else
                    {
                        MessageBox.Show("Šis darbuotojas įtrauktas į užsakymus, todėl ištrynus gali dingti reikalinga informacija");
                    }
                    break;
                case "Prekė":
                    d = Metodai.Detalės.FirstOrDefault(stringToCheck => stringToCheck.PrekėsID.Contains(Metodai.Prekės.FirstOrDefault(stringCheck => stringCheck.Pavadinimas.Contains(Pavadinimas_combo.Text)).ID));
                    if (d == null)
                    {
                        File.Delete(Metodai.failai[2]);
                        for (int i = 0; i < Metodai.Prekės.Count; i++)
                        {
                            if (Pavadinimas_combo.Text.Equals(Metodai.Prekės[i].Pavadinimas))
                                Metodai.Prekės.RemoveAt(i);
                            if (i != Metodai.Prekės.Count)
                                Metodai.Salinimas(Metodai.Prekės[i], 2);
                        }
                        istrinta = true;
                    }
                    else
                    {
                        MessageBox.Show("Ši prekė įtraukta į užsakymus, todėl ištrynus gali dingti reikalinga informacija");
                    }
                    break;
                case "Išvalyti duomenis":
                    if (MessageBox.Show("Ar tikrai norite pašalinti šiuos duomenis?", "Patvirtinti", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        switch (Pavadinimas_combo.Text)
                        {
                            case "Užsakymai":
                                File.Create(Metodai.failai[0]);
                                break;
                            case "Detalizacija":
                                File.Create(Metodai.failai[1]);
                                break;
                            case "Prekės":
                                File.Create(Metodai.failai[2]);
                                break;
                            case "Darbuotojai":
                                File.Create(Metodai.failai[3]);
                                break;
                            case "Klientai":
                                File.Create(Metodai.failai[4]);
                                break;
                        }
                        MessageBox.Show("Duomenys pašalinti sėkmingai");
                    }
                    break;
            }
            if (istrinta)
                MessageBox.Show("Elementas ištryntas");
            Kategorija_combo.SelectedIndex = 0;
            Remove.IsEnabled = false;
        }

        /// <summary>
        /// Išjungia ši langa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
