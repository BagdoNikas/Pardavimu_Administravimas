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

//------------------------------------------------------------------
//---- prideti reikia nuget packages: ------------------------------
//---- Microsoft.Office.Interop.Word  ------------------------------
//---- DocX                           ------------------------------
//------------------------------------------------------------------

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
            Formatas_combo.Items.Add(".pdf");
            Formatas_combo.Items.Add(".docx");

            Pasirinkimas_combo.SelectionChanged += SelectionChanged;
            Pasirinkimas_combo.Items.Add("----------------");
            Pasirinkimas_combo.Items.Add("Metinė ataskaita");
            Pasirinkimas_combo.Items.Add("Pagal klientą");
            Pasirinkimas_combo.Items.Add("Pagal darbuotoją");
            Pasirinkimas_combo.Items.Add("Pagal datą");
            
        }

        //string docxName = "";
        //string pdfDocName = "";
        string fileName = "";


        /// <summary>
        /// Iškviečia ataskaitos formatavimo metodus pagal combobox pasirinkimą
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Formuoti_Click(object sender, RoutedEventArgs e)
        {
            string formatas = Formatas_combo.SelectedItem.ToString();
            string path = Direktorija();
            

            if (Formatas_combo.IsLoaded)
            {
                switch (formatas)
                {
                    case ".pdf":
                        fileName = path + '\\' + Name_textbox.Text;
                        Pdf();
                        Process.Start(fileName);
                        break;
                    case ".docx":
                        fileName = path + '\\' + Name_textbox.Text + ".docx";
                        Docx(fileName);
                        Process.Start(fileName);
                        break;
                }
            }

            //pdfDocName += path + '\\' + Name_textbox.Text + ".pdf";
            //docxName += path + '\\' + Name_textbox.Text + ".docx";

            //switch (formatas)
            //{
            //    case ".pdf":
            //        Pdf(); Process.Start(pdfDocName); break;
            //    case ".docx":
            //        Docx(docxName); Process.Start(docxName); break;
            //}

            this.Close();
        }

        /// <summary>
        /// Uždaro šį langą
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Sukuria Pdf faila pries tai iskvieciant metoda sukurti docx failui tada istrina docx faila
        /// </summary>
        public void Pdf()
        {
            string doc = fileName + ".docx";
            string pdf = fileName + ".pdf";

            Docx(doc);

            var app = new Word.Application();
            if (app.Documents != null)
            {
                var wordDocument = app.Documents.Open(doc);
                //-----------Cia pakeist (PREFERABLY SAVE THE NAME THE USER WRITES)------------
                if (wordDocument != null)
                {
                    wordDocument.ExportAsFixedFormat(pdf, Word.WdExportFormat.wdExportFormatPDF);
                    wordDocument.Close();
                }
                app.Quit();
            }
            File.Delete(doc);
        }

        /// <summary>
        /// Sukuria docx faila ir iraso duomenis
        /// </summary>
        /// <param name="docx"></param>
        public void Docx(string docx)
        {
            //------Cia sukuria nauja docx failą-----//
            var doc = DocX.Create(docx);

            //-------------------Link kur parodo kaip formatuoti viska --------------------//
            // https://www.c-sharpcorner.com/article/generate-word-document-using-c-sharp/ //
            //-----------------------------------------------------------------------------//

            #region Teksto nustatymai
            Formatting textFormat = new Formatting();
            textFormat.Size = 12;
            Formatting titleFormat = new Formatting();
            titleFormat.Size = 18;
            #endregion


            if (Pasirinkimas_combo.IsLoaded)
            {
                switch (Pasirinkimas_combo.SelectedIndex)
                {
                    case 1:
                        #region Pavadinimo nustatymai
                        string title = Criteria_combo.Text + "\n Metinė Ataskaita";
                        Paragraph Title = doc.InsertParagraph(title, false, titleFormat);
                        Title.Alignment = Alignment.center;
                        #endregion

                        #region Lenteles nustatymai
                        Table t = doc.AddTable(Metodai.Užsakymai.Count + 1, 6);
                        t.Alignment = Alignment.center;
                        t.Design = TableDesign.TableGrid;

                        for (int i = 0; i < 6; i++)
                            t.Rows[0].Cells[i].FillColor = Color.LightGray;
                        t.SetWidthsPercentage(new[] { 10f, 30f, 30f, 15f, 15f }, 600);
                        t.Rows[0].Cells[0].Paragraphs.First().Append("Nr.");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("Pirkėjas");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("Pardavėjas");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Prekė ir kiekis");
                        t.Rows[0].Cells[4].Paragraphs.First().Append("Data");
                        t.Rows[0].Cells[5].Paragraphs.First().Append("Suma");
                        #endregion

                        #region sukuria elementus lentelems / sugeneruoja lentelę
                        // k - papildomas kintamasis pereiti i kita eilute nes keletas prekiu vienam uzsakymui
                        // l - kad graziai kiekviena preke susidetu i atskiras eilutes, nenaudojant kas kart pasikeitus uzsakymui atsirastu tuscia eilute
                        int /*k = 1,*/ e = 0;//, l = 1;
                        //double kaina = 0;
                        //double prekeskaina;
                        for (int i = 0; i < Metodai.Užsakymai.Count; i++)
                        {
                            //l = 1;
                            e = i + 1;
                            t.Rows[e].Cells[0].Paragraphs.First().Append(Metodai.Užsakymai[i].ID);
                            t.Rows[e].Cells[1].Paragraphs.First().Append(Metodai.Užsakymai[i].Pirkėjas);
                            t.Rows[e].Cells[2].Paragraphs.First().Append(Metodai.Užsakymai[i].Darbuotojas);
                            for (int j = 0; j < Metodai.Detalės[i].PrekėsID.Length; j++)
                            {
                                t.Rows[e].Cells[3].Paragraphs.First().Append(Metodai.Detalės[i].PrekėsID[j] + " ");
                                t.Rows[e].Cells[3].Paragraphs.First().Append(Metodai.Detalės[i].Kiekis[j].ToString() + "\n");
                                #region
                                //t.Rows[i + k].Cells[0].Paragraphs.First().Append(Metodai.Užsakymai[i].ID);
                                //t.Rows[i + k].Cells[1].Paragraphs.First().Append(Metodai.Užsakymai[i].Pirkėjas);
                                //t.Rows[i + k].Cells[2].Paragraphs.First().Append(Metodai.Užsakymai[i].Darbuotojas);
                                //--
                                //t.Rows[i + k].Cells[3].Paragraphs.First().Append(Metodai.Detalės[i].PrekėsID[j]);
                                //t.Rows[i + k].Cells[4].Paragraphs.First().Append(Metodai.Detalės[i].Kiekis[j].ToString());
                                //--
                                //t.Rows[i + k].Cells[5].Paragraphs.First().Append(Metodai.Užsakymai[i].Data.ToString());
                                //Kodel padarei dauginima prekes x kiekio? Juk uzsakyme jau yra kaina
                                //prekeskaina = Metodai.Prekės.FirstOrDefault(stringToCheck => stringToCheck.ID.Contains(Metodai.Detalės[i].PrekėsID[j])).Kaina;
                                //kaina = Metodai.Detalės[i].Kiekis[j] * prekeskaina;
                                //t.Rows[i + k].Cells[6].Paragraphs.First().Append(kaina.ToString());
                                //t.Rows[i + k].Cells[6].Paragraphs.First().Append(Metodai.Užsakymai[i].Suma.ToString());
                                //if (l != Metodai.Detalės[i].PrekėsID.Length)
                                //{
                                //    l++;
                                //    k++;
                                //}
                                #endregion
                            }
                            t.Rows[e].Cells[4].Paragraphs.First().Append(Metodai.Užsakymai[i].Data.ToString());
                            t.Rows[e].Cells[5].Paragraphs.First().Append(Metodai.Užsakymai[i].Suma.ToString());
                        }
                        #endregion
                        doc.InsertTable(t);
                        break;
                    case 2:
                        #region Pavadinimo nustatymai
                        title = Criteria_combo.Text + "\n Kliento Ataskaita";
                        Title = doc.InsertParagraph(title, false, titleFormat);
                        Title.Alignment = Alignment.center;
                        #endregion

                        #region Lenteles nustatymai
                        t = doc.AddTable(1 + Metodai.prekiuskaicius, 7);
                        t.Alignment = Alignment.center;
                        t.Design = TableDesign.TableGrid;

                        for (int i = 0; i < 7; i++)
                            t.Rows[0].Cells[i].FillColor = Color.LightGray;
                        t.SetWidthsPercentage(new[] { 7f, 30f, 20f, 15f, 15f, 13f }, 600);
                        t.Rows[0].Cells[0].Paragraphs.First().Append("Nr.");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("Pirkėjas");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("Pardavėjas");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Prekė");
                        t.Rows[0].Cells[4].Paragraphs.First().Append("Kiekis");
                        t.Rows[0].Cells[5].Paragraphs.First().Append("Data");
                        t.Rows[0].Cells[6].Paragraphs.First().Append("Suma");
                        #endregion
                        break;
                    case 3:
                        #region Pavadinimo nustatymai
                        title = Criteria_combo.Text + "\n Darbuotojo Ataskaita";
                        Title = doc.InsertParagraph(title, false, titleFormat);
                        Title.Alignment = Alignment.center;
                        #endregion
                        break;
                    case 4:
                        #region Pavadinimo nustatymai
                        title = Criteria_combo.Text + "\n Ataskaita";
                        Title = doc.InsertParagraph(title, false, titleFormat);
                        Title.Alignment = Alignment.center;
                        #endregion
                        break;
                }
            }
            
            doc.Save();
        }

        /// <summary>
        /// Leidzia vartotojui pasirinkti direktorija i kuria isaugoti ataskaita
        /// </summary>
        /// <returns></returns>
        public string Direktorija()
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            string name = dialog.SelectedPath;
            return name;
        }

        private void TextIsChanged(object sender, TextChangedEventArgs e)
        {
            if (Name_textbox.IsLoaded && Criteria_combo.IsLoaded && Pasirinkimas_combo.IsLoaded)
                if (Name_textbox.Text.Length > 0 && Pasirinkimas_combo.SelectedIndex > 0 && Criteria_combo.Text.Length > 0)
                    formuoti.IsEnabled = true;
                else
                    formuoti.IsEnabled = false;
        }

        private void TextIsChanged()
        {
            if (Name_textbox.IsLoaded && Criteria_combo.IsLoaded && Pasirinkimas_combo.IsLoaded)
                if (Name_textbox.Text.Length > 0 && Pasirinkimas_combo.SelectedIndex > 0 && Criteria_combo.Text.Length > 0)
                    formuoti.IsEnabled = true;
                else
                    formuoti.IsEnabled = false;
        }

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
                        Criteria_combo.AllowDrop = false;
                        Criteria_combo.IsEditable = true;
                        break;
                }
                Criteria_combo.Items.Refresh();
                Criteria_combo.SelectedIndex = 0;
                TextIsChanged();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("1. Pasirinkite ataskaitos formatą.\n" +
                                           "2. Įveskite ataskaitos pavadinimą.\n" +
                                           "3. Pasirinkite vietą, į kurią norite išsaugoti ataskaitos dokumentą.", "Pagalba");
        }
    }
}
