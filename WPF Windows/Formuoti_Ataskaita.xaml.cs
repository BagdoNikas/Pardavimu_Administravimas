using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Xceed.Words.NET;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;

//------------------ Bug'ai ------------------------
// Kai išmeta direktorijos pasirinkimą, paspaudus atšaukti meta errora
// PDF failo kūrime Mariaus parašytas komentaras. No idea kas ten
// Tarpsnio ataskaita. ComboBox'as galėtų būti kaip TextBox'as
//--------------------------------------------------

namespace Administravimas.WPF_Windows
{
    /// <summary>
    /// Interaction logic for Formuoti_Ataskaita.xaml
    /// </summary>
    public partial class Formuoti_Ataskaita : Window
    {

        public Formuoti_Ataskaita()
        {
            InitializeComponent();
            //Automatiškai užpildo formato pasirinkimo comboBox'ą
            Formatas_combo.Items.Add(".pdf");
            Formatas_combo.Items.Add(".docx");
            //Automatiškai užpildo apibendrinto kriterijaus comboBox'ą
            Pasirinkimas_combo.SelectionChanged += SelectionChanged;
            Pasirinkimas_combo.Items.Add("----------------");
            Pasirinkimas_combo.Items.Add("Metinė ataskaita");
            Pasirinkimas_combo.Items.Add("Pagal klientą");
            Pasirinkimas_combo.Items.Add("Pagal darbuotoją");
            Pasirinkimas_combo.Items.Add("Pagal datą");
        }

        //Word dokumento ir PDF failo kelių kintamieji
        string docx = "";
        string pdf = "";

        /// <summary>
        /// Paspaudus mygtuką "Formuoti", pagal pasirinkimus suformuoja ataskaitą.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Formuoti_Click(object sender, RoutedEventArgs e)
        {
            string formatas = Formatas_combo.SelectedItem.ToString();
            string path = Direktorija();
            docx = path + '\\' + Name_textbox.Text + ".docx";
            pdf = path + '\\' + Name_textbox.Text + ".pdf";
            if (File.Exists(docx) || File.Exists(pdf))
                if (System.Windows.MessageBox.Show("Toks failas jau egzistuoja.\nAr norite perašyti fąilo duomenis?.", "Įspėjimas", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (Formatas_combo.IsLoaded)
                    {
                        switch (formatas)
                        {
                            case ".pdf":
                                Pdf();
                                if (File.Exists(pdf))
                                    Process.Start(pdf);
                                break;
                            case ".docx":
                                Docx(docx);
                                if (File.Exists(docx))
                                    Process.Start(docx);
                                break;
                        }
                    }

                    this.Close();
                }
        }

        /// <summary>
        /// Lango uždarymas "Atšaukti" mygtuko paspaudimu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// PDF failo sukūrimo metodas, naudojantis Word dokumento kūrimo metodą. .docx yra konvertuojamas į .pdf, tada ištrinamas.
        /// </summary>
        public void Pdf()
        {
            Docx(docx);
            var app = new Word.Application();
            if (File.Exists(docx))
            {
                var wordDocument = app.Documents.Open(docx);
                //-----------Cia pakeist (PREFERABLY SAVE THE NAME THE USER WRITES)------------
                if (wordDocument != null)
                {
                    wordDocument.ExportAsFixedFormat(pdf, Word.WdExportFormat.wdExportFormatPDF);
                    wordDocument.Close();
                }
                app.Quit();
            }
            File.Delete(docx);
        }

        /// <summary>
        /// Word dokumento kūrimo metodas.
        /// Dokumentas generuojamas pagal parinktus kriterijus.
        /// Nuoroda: https://www.c-sharpcorner.com/article/generate-word-document-using-c-sharp/
        /// </summary>
        /// <param name="docx">Word dokumento direktorija</param>
        public void Docx(string docx)
        {
            var doc = DocX.Create(docx);
            #region Teksto nustatymai
            Formatting textFormat = new Formatting();
            textFormat.Size = 12;
            Formatting titleFormat = new Formatting();
            titleFormat.Size = 18;
            Formatting header1 = new Formatting();
            header1.Size = 16;
            Formatting header2 = new Formatting();
            header1.Size = 14;
            #endregion
            if (Pasirinkimas_combo.IsLoaded)
            {
                switch (Pasirinkimas_combo.SelectedIndex)
                {
                    case 1:
                        #region Tikrinimas
                        int count = 0;
                        for (int i = 0; i < Metodai.Užsakymai.Count; i++)
                            if (Metodai.Užsakymai[i].Data.Year.ToString() == Criteria_combo.Text)
                                count++;
                        if (count == 0)
                        {
                            System.Windows.MessageBox.Show("Tokiais metais nebuvo pateiktas nė vienas užsakymas.\nDokumentas nebus sukurtas.", "Įspėjimas");
                            File.Delete(docx);
                            break;
                        }
                        #endregion
                        #region Pavadinimo nustatymai
                        string title = Criteria_combo.Text;
                        string head1 = "Metinė Ataskaita";
                        string head2 = "Relatively Interesting Project Makers Dummy Firm\n";
                        Paragraph Title = doc.InsertParagraph(title, false, titleFormat);
                        Paragraph Header1 = doc.InsertParagraph(head1, false, header1);
                        Paragraph Header2 = doc.InsertParagraph(head2, false, header2);
                        Title.Alignment = Alignment.center;
                        Header1.Alignment = Alignment.center;
                        Header2.Alignment = Alignment.center;
                        #endregion
                        #region Lenteles ir jos antraštės nustatymai
                        Table t = doc.AddTable(count + 1, 6);
                        t.Alignment = Alignment.center;
                        t.Design = TableDesign.TableGrid;
                        for (int i = 0; i < 6; i++)
                            t.Rows[0].Cells[i].FillColor = Color.LightGray;
                        t.SetWidthsPercentage(new[] { 9f, 25f, 25f, 13f, 15f, 9f }, 600);
                        t.Rows[0].Cells[0].Paragraphs.First().Append("Nr.");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("Pirkėjas");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("Pardavėjas");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Prekė ir kiekis");
                        t.Rows[0].Cells[4].Paragraphs.First().Append("Data");
                        t.Rows[0].Cells[5].Paragraphs.First().Append("Suma");
                        #endregion
                        #region Lentelės pildymas duomenimis
                        int e = -1;
                        for (int i = 0; i < Metodai.Užsakymai.Count; i++)
                            if (Metodai.Užsakymai[i].Data.Year.ToString() == Criteria_combo.Text)
                            {
                                e = i + 1;
                                t.Rows[e].Cells[0].Paragraphs.First().Append(Metodai.Užsakymai[i].ID);
                                t.Rows[e].Cells[1].Paragraphs.First().Append(Metodai.Užsakymai[i].Pirkėjas);
                                t.Rows[e].Cells[2].Paragraphs.First().Append(Metodai.Užsakymai[i].Darbuotojas);
                                for (int j = 0; j < Metodai.Detalės[i].PrekėsID.Length; j++)
                                {
                                    t.Rows[e].Cells[3].Paragraphs.First().Append(Metodai.Detalės[i].PrekėsID[j] + " ");
                                    t.Rows[e].Cells[3].Paragraphs.First().Append(Metodai.Detalės[i].Kiekis[j].ToString());
                                    if (j + 1 < Metodai.Detalės[i].PrekėsID.Length)
                                        t.Rows[e].Cells[3].Paragraphs.First().Append("\n");
                                }
                                t.Rows[e].Cells[4].Paragraphs.First().Append(Metodai.Užsakymai[i].Data.ToString("yyyy-MM-dd hh:mm"));
                                t.Rows[e].Cells[5].Paragraphs.First().Append(Metodai.Užsakymai[i].Suma.ToString());
                            }
                        #endregion
                        doc.InsertTable(t);
                        doc.Save();
                        break;
                    case 2:
                        #region Tikrinimas
                        count = 0;
                        for (int i = 0; i < Metodai.Užsakymai.Count; i++)
                            if (Metodai.Užsakymai[i].Pirkėjas.Equals(Criteria_combo.Text))
                                count++;
                        if (count == 0)
                        {
                            System.Windows.MessageBox.Show("Šis pirkėjas nepateikė nė vieno užsakymo.\nDokumentas nebus sukurtas.", "Įspėjimas");
                            File.Delete(docx);
                            break;
                        }
                        #endregion
                        #region Pavadinimo nustatymai
                        title = Criteria_combo.Text;
                        head1 = "Kliento Ataskaita";
                        head2 = "Relatively Interesting Project Makers Dummy Firm\n";
                        Title = doc.InsertParagraph(title, false, titleFormat);
                        Header1 = doc.InsertParagraph(head1, false, header1);
                        Header2 = doc.InsertParagraph(head2, false, header2);
                        Title.Alignment = Alignment.center;
                        Header1.Alignment = Alignment.center;
                        Header2.Alignment = Alignment.center;
                        #endregion
                        #region Lenteles ir jos antraštės nustatymai                        
                        t = doc.AddTable(count + 1, 5);
                        t.Alignment = Alignment.center;
                        t.Design = TableDesign.TableGrid;
                        for (int i = 0; i < 5; i++)
                            t.Rows[0].Cells[i].FillColor = Color.LightGray;
                        t.SetWidthsPercentage(new[] { 10f, 30f, 20f, 30f, 10f }, 600);
                        t.Rows[0].Cells[0].Paragraphs.First().Append("Užsakymo Nr.");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("Pardavėjas");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("Prekė ir kiekis");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Data");
                        t.Rows[0].Cells[4].Paragraphs.First().Append("Suma");
                        #endregion
                        #region Lentelės pildymas duomenimis
                        e = 1;
                        for (int i = 0; i < Metodai.Užsakymai.Count; i++)
                            if (Metodai.Užsakymai[i].Pirkėjas.Equals(Criteria_combo.Text))
                            {
                                t.Rows[e].Cells[0].Paragraphs.First().Append(Metodai.Užsakymai[i].ID);
                                t.Rows[e].Cells[1].Paragraphs.First().Append(Metodai.Užsakymai[i].Darbuotojas);
                                for (int j = 0; j < Metodai.Detalės[i].PrekėsID.Length; j++)
                                {
                                    t.Rows[e].Cells[2].Paragraphs.First().Append(Metodai.Detalės[i].PrekėsID[j] + " ");
                                    t.Rows[e].Cells[2].Paragraphs.First().Append(Metodai.Detalės[i].Kiekis[j].ToString());
                                    if (j + 1 < Metodai.Detalės[i].PrekėsID.Length)
                                        t.Rows[e].Cells[2].Paragraphs.First().Append("\n");
                                }
                                t.Rows[e].Cells[3].Paragraphs.First().Append(Metodai.Užsakymai[i].Data.ToString("yyyy-MM-dd hh:mm"));
                                t.Rows[e].Cells[4].Paragraphs.First().Append(Metodai.Užsakymai[i].Suma.ToString());
                                e++;
                            }
                        #endregion
                        doc.InsertTable(t);
                        doc.Save();
                        break;
                    case 3:
                        #region Tikrinimas
                        count = 0;
                        for (int i = 0; i < Metodai.Užsakymai.Count; i++)
                            if (Metodai.Užsakymai[i].Darbuotojas.Equals(Criteria_combo.Text))
                                count++;
                        if (count == 0)
                        {
                            System.Windows.MessageBox.Show("Šis darbuotojas nepriėmė nė vieno užsakymo.\nDokumentas nebus sukurtas.", "Įspėjimas");
                            File.Delete(docx);
                            break;
                        }
                        #endregion
                        #region Pavadinimo nustatymai
                        title = Criteria_combo.Text;
                        head1 = "Darbuotojo Ataskaita";
                        head2 = "Relatively Interesting Project Makers Dummy Firm\n";
                        Title = doc.InsertParagraph(title, false, titleFormat);
                        Header1 = doc.InsertParagraph(head1, false, header1);
                        Header2 = doc.InsertParagraph(head2, false, header2);
                        Title.Alignment = Alignment.center;
                        Header1.Alignment = Alignment.center;
                        Header2.Alignment = Alignment.center;
                        #endregion
                        #region Lenteles ir jos antraštės nustatymai
                        t = doc.AddTable(count + 1, 5);
                        t.Alignment = Alignment.center;
                        t.Design = TableDesign.TableGrid;
                        for (int i = 0; i < 5; i++)
                            t.Rows[0].Cells[i].FillColor = Color.LightGray;
                        t.SetWidthsPercentage(new[] { 10f, 30f, 20f, 30f, 10f }, 600);
                        t.Rows[0].Cells[0].Paragraphs.First().Append("Užsakymo Nr.");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("Pirkėjas");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("Prekė ir kiekis");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Data");
                        t.Rows[0].Cells[4].Paragraphs.First().Append("Suma");
                        #endregion
                        #region Lentelės pildymas duomenimis
                        e = 1;
                        for (int i = 0; i < Metodai.Užsakymai.Count; i++)
                            if (Metodai.Užsakymai[i].Darbuotojas.Equals(Criteria_combo.Text))
                            {
                                t.Rows[e].Cells[0].Paragraphs.First().Append(Metodai.Užsakymai[i].ID);
                                t.Rows[e].Cells[1].Paragraphs.First().Append(Metodai.Užsakymai[i].Pirkėjas);
                                for (int j = 0; j < Metodai.Detalės[i].PrekėsID.Length; j++)
                                {
                                    t.Rows[e].Cells[2].Paragraphs.First().Append(Metodai.Detalės[i].PrekėsID[j] + " ");
                                    t.Rows[e].Cells[2].Paragraphs.First().Append(Metodai.Detalės[i].Kiekis[j].ToString());
                                    if (j + 1 < Metodai.Detalės[i].PrekėsID.Length)
                                        t.Rows[e].Cells[2].Paragraphs.First().Append("\n");
                                }
                                t.Rows[e].Cells[3].Paragraphs.First().Append(Metodai.Užsakymai[i].Data.ToString("yyyy-MM-dd hh:mm"));
                                t.Rows[e].Cells[4].Paragraphs.First().Append(Metodai.Užsakymai[i].Suma.ToString());
                                e++;
                            }
                        #endregion
                        doc.InsertTable(t);
                        doc.Save();
                        break;
                    case 4:
                        #region Tikrinimas
                        string[] dates = Criteria_combo.Text.Split(' ');
                        count = 0;
                        for (int i = 0; i < Metodai.Užsakymai.Count; i++)
                            if (Metodai.Užsakymai[i].Data >= Convert.ToDateTime(dates[0]) && Metodai.Užsakymai[i].Data <= Convert.ToDateTime(dates[1])) //Reikia issiaskinti kodel imtinai neimai antros ribos
                                count++;
                        if (count == 0)
                        {
                            System.Windows.MessageBox.Show("Šiuo laikotarpiu nepriimtas nė vienas užsakymas.\nDokumentas nebus sukurtas.", "Įspėjimas");
                            File.Delete(docx);
                            break;
                        }
                        #endregion
                        #region Pavadinimo nustatymai
                        title = Criteria_combo.Text;
                        head1 = "Tarpsnio Ataskaita";
                        head2 = "Relatively Interesting Project Makers Dummy Firm\n";
                        Title = doc.InsertParagraph(title, false, titleFormat);
                        Header1 = doc.InsertParagraph(head1, false, header1);
                        Header2 = doc.InsertParagraph(head2, false, header2);
                        Title.Alignment = Alignment.center;
                        Header1.Alignment = Alignment.center;
                        Header2.Alignment = Alignment.center;
                        #endregion
                        #region Lenteles ir jos antraštės nustatymai
                        t = doc.AddTable(count + 1, 6);
                        t.Alignment = Alignment.center;
                        t.Design = TableDesign.TableGrid;
                        for (int i = 0; i < 6; i++)
                            t.Rows[0].Cells[i].FillColor = Color.LightGray;
                        t.SetWidthsPercentage(new[] { 8f, 25f, 25f, 14f, 20f, 8f }, 600);
                        t.Rows[0].Cells[0].Paragraphs.First().Append("Užsakymo Nr.");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("Pirkėjas");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("Pardavėjas");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Prekė ir kiekis");
                        t.Rows[0].Cells[4].Paragraphs.First().Append("Data");
                        t.Rows[0].Cells[5].Paragraphs.First().Append("Suma");
                        #endregion
                        #region Lentelės pildymas duomenimis
                        e = 1;
                        for (int i = 0; i < Metodai.Užsakymai.Count; i++)
                            if (Metodai.Užsakymai[i].Data >= Convert.ToDateTime(dates[0]) && Metodai.Užsakymai[i].Data <= Convert.ToDateTime(dates[1]))
                            {
                                t.Rows[e].Cells[0].Paragraphs.First().Append(Metodai.Užsakymai[i].ID);
                                t.Rows[e].Cells[1].Paragraphs.First().Append(Metodai.Užsakymai[i].Pirkėjas);
                                t.Rows[e].Cells[2].Paragraphs.First().Append(Metodai.Užsakymai[i].Darbuotojas);
                                for (int j = 0; j < Metodai.Detalės[i].PrekėsID.Length; j++)
                                {
                                    t.Rows[e].Cells[3].Paragraphs.First().Append(Metodai.Detalės[i].PrekėsID[j] + " ");
                                    t.Rows[e].Cells[3].Paragraphs.First().Append(Metodai.Detalės[i].Kiekis[j].ToString());
                                    if (j + 1 < Metodai.Detalės[i].PrekėsID.Length)
                                        t.Rows[e].Cells[3].Paragraphs.First().Append("\n");
                                }
                                t.Rows[e].Cells[4].Paragraphs.First().Append(Metodai.Užsakymai[i].Data.ToString("yyyy-MM-dd hh:mm"));
                                t.Rows[e].Cells[5].Paragraphs.First().Append(Metodai.Užsakymai[i].Suma.ToString());
                                e++;
                            }
                        #endregion
                        doc.InsertTable(t);
                        doc.Save();
                        break;
                }
            }     
        }

        /// <summary>
        /// Interaktyvaus direktorijos parinkimo metodas.
        /// </summary>
        /// <returns></returns>
        public string Direktorija()
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            string name = dialog.SelectedPath;
            return name;
        }

        /// <summary>
        /// Mygtuko "Formuoti" įgalinimui reikalingi veiksmai, kai pakeičiamas pavadinimas.
        /// WPF formos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextIsChanged(object sender, TextChangedEventArgs e)
        {
            if (Name_textbox.IsLoaded && Criteria_combo.IsLoaded && Pasirinkimas_combo.IsLoaded)
                if (Name_textbox.Text.Length > 0 && Pasirinkimas_combo.SelectedIndex > 0 && Criteria_combo.Text.Length > 0)
                    formuoti.IsEnabled = true;
                else
                    formuoti.IsEnabled = false;
        }

        /// <summary>
        /// Mygtuko "Formatuoti" įgalinimui reikalingi veiksmai, kai pakeičiamos reikšmės ComboBox'uose ir TextBox'uose.
        /// Reikalingas kai forma pildoma nenuosekliai.
        /// </summary>
        private void TextIsChanged()
        {
            if (Name_textbox.IsLoaded && Criteria_combo.IsLoaded && Pasirinkimas_combo.IsLoaded)
                if (Name_textbox.Text.Length > 0 && Pasirinkimas_combo.SelectedIndex > 0 && Criteria_combo.Text.Length > 0)
                    formuoti.IsEnabled = true;
                else
                    formuoti.IsEnabled = false;
        }

        /// <summary>
        /// Komponento "Kriterijus" pildymas, priklausomai nuo "Kriterijaus pasirinkimas" reikšmės.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Pasirinkimas_combo.IsLoaded)
            {
                switch (Pasirinkimas_combo.SelectedIndex)
                {
                    case 0:
                        Criteria_combo.Items.Clear();
                        Criteria_combo.IsEditable = false;
                        Criteria_combo.IsHitTestVisible = false;
                        break;
                    case 1:
                        Criteria_combo.Items.Clear();
                        Criteria_combo.IsEditable = true;
                        Criteria_combo.IsHitTestVisible = true;
                        for (int i = DateTime.Now.Year; i >= 1998; i--)
                        {
                            Criteria_combo.Items.Add(i.ToString());
                        }
                        break;
                    case 2:
                        Criteria_combo.Items.Clear();
                        Criteria_combo.IsEditable = false;
                        Criteria_combo.IsHitTestVisible = true;
                        for (int i = 0; i < Metodai.Klientai.Count; i++)
                        {
                            Criteria_combo.Items.Add(Metodai.Klientai[i].Pavadinimas);
                        }
                        break;
                    case 3:
                        Criteria_combo.Items.Clear();
                        Criteria_combo.IsEditable = false;
                        Criteria_combo.IsHitTestVisible = true;
                        for (int i = 0; i < Metodai.Pardavėjai.Count; i++)
                        {
                            Criteria_combo.Items.Add(Metodai.Pardavėjai[i].VardasPavardė);
                        }
                        break;
                    case 4:
                        Criteria_combo.Items.Clear();
                        //reiketu padaryti kad komboboxas butu kaip textboxas, t.y. kazkaip disable dropdown lista
                        Criteria_combo.IsEditable = true;
                        break;
                }
                Criteria_combo.Items.Refresh();
                Criteria_combo.SelectedIndex = 0;
                TextIsChanged();
            }
        }

        /// <summary>
        /// Mygtuko "Pagalba" atliekami veiksmai.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("1. Įveskite ataskaitos pavadinimą.\n" +
                                           "2. Pasirinkite ataskaitos formatą.\n" +
                                           "3. Pasirinkite apibendrintą kriterijų.\n" +
                                           "4. Patikslinkite kriterijų:\n" +
                                           "   4.1. Metinei ataskaitai parinkti arba įvesti metus.\n" +
                                           "   4.2. Kliento ataskaitai parinkti klientą.\n" +
                                           "   4.3. Darbuotojo ataskaitai parinkti darbuotoją.\n" +
                                           "   4.4. Tarpsnio ataskaitai įvesti datų intervalą.\n" +
                                           "        Formatas: \"yyyy-MM-dd yyyy-MM-dd\" .\n" +
                                           "5. Pasirinkite vietą, į kurią norite išsaugoti ataskaitos dokumentą.", "Pagalba");
        }
    }
}
