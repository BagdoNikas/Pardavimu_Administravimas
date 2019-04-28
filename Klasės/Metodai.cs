using System;
using Microsoft.Win32;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Administravimas
{
    /// <summary>
    /// Metodų aprašymo klasė
    /// </summary>
    public class Metodai
    {
        #region Kintamieji ir Listai
        //-----------------Duomenų saugojimo sąrašai------------------------
        public static List<Užsakymas> Užsakymai = new List<Užsakymas>();
        public static List<Detalizacija> Detalės = new List<Detalizacija>();
        public static List<Prekė> Prekės = new List<Prekė>();
        public static List<Klientas> Klientai = new List<Klientas>();
        public static List<Pardavėjas> Pardavėjai = new List<Pardavėjas>();

        //-----------------Duomenų failų konstantos-------------------------
        const string UžDataFile = "..\\..\\Failai\\Užsakymai.txt";
        const string DeDataFile = "..\\..\\Failai\\Detalizacija.txt";
        const string PDataFile = "..\\..\\Failai\\Prekės.txt";
        const string DaDataFile = "..\\..\\Failai\\Darbuotojai.txt";
        const string KlDataFile = "..\\..\\Failai\\Klientai.txt";

        public static string[] failai = { UžDataFile, DeDataFile, PDataFile, DaDataFile, KlDataFile };
        //-----------------Generuojamų ID kintamieji------------------------
        public static string id_klientoJ;
        public static string id_klientoF;
        public static string id_darbuotojo;
        public static string id_uzsakymo;
        public static string id_prekes;
        public static int prekiuskaicius = 0;
        #endregion

        /// <summary>
        /// Duomenų nuskaitymo iš failų metodas
        /// </summary>
        public static void Skaitymas()
        {            
            //-------------------------Užsakymai----------------------------
            using (StreamReader reader = new StreamReader(UžDataFile, Encoding.GetEncoding(1257)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] part = line.Split(';');
                    id_uzsakymo = part[0];
                    string klientas = part[1];
                    double suma = double.Parse(part[2]);
                    string pardavėjas = part[3];
                    DateTime data = DateTime.Parse(part[4]);
                    Užsakymas dummy = new Užsakymas(id_uzsakymo, klientas, suma, pardavėjas, data);
                    Užsakymai.Add(dummy);
                }
            }

            //-------------------------Detalizacija-------------------------
            using (StreamReader reader = new StreamReader(DeDataFile, Encoding.GetEncoding(1257)))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] part = line.Split(';');
                    string uid = part[0];
                    int i = 1;
                List<string> pid = new List<string>();
                List<short> kiekiai = new List<short>();
                    while (part[i] != "")
                    {
                        string pidnkiekis = part[i];
                        string[] pikd = pidnkiekis.Split(' ');
                        string id = pikd[0];
                        short kiekis = short.Parse(pikd[1]);
                        pid.Add(id);
                        kiekiai.Add(kiekis);
                        i++;
                        prekiuskaicius++;
                    }
                    Detalizacija dummy = new Detalizacija(uid, pid.ToArray(), kiekiai.ToArray());
                    Detalės.Add(dummy);
                }
            }

            //-------------------------Prekės-------------------------------
            using (StreamReader reader = new StreamReader(PDataFile, Encoding.GetEncoding(1257)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] part = line.Split(';');
                    id_prekes = part[0];
                    string pav = part[1];
                    double kain = double.Parse(part[2]);
                    Prekė dummy = new Prekė(id_prekes, pav, kain);
                    Prekės.Add(dummy);
                }
            }

            //-------------------------Klientai-----------------------------
            using (StreamReader reader = new StreamReader(KlDataFile, Encoding.GetEncoding(1257)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] part = line.Split(';');
                    string tipas = part[1];
                    string pav = part[2];
                    long kodas = long.Parse(part[3]);
                    long tel = long.Parse(part[4]);

                    Klientas dummy = new Klientas();
                    switch (part[0][0])
                    {
                        case 'F': id_klientoF = part[0]; dummy = new Klientas(id_klientoF, tipas, pav, kodas, tel); break;
                        case 'J': id_klientoJ = part[0]; dummy = new Klientas(id_klientoJ, tipas, pav, kodas, tel); break;
                    }
                    Klientai.Add(dummy);
                }
            }

            //-------------------------Darbuotojai--------------------------
            using (StreamReader reader = new StreamReader(DaDataFile, Encoding.GetEncoding(1257)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] part = line.Split(';');
                    id_darbuotojo = part[0];
                    string varpav = part[1];
                    Pardavėjas dummy = new Pardavėjas(id_darbuotojo, varpav);
                    Pardavėjai.Add(dummy);
                }
            }
        }

        /// <summary>
        /// Naujo užsakymo formavimo metodas
        /// </summary>
        /// <param name="id">užsakymo identifikatorius</param>
        /// <param name="klientas">kliento vardas ir pavardė arba pavadinimas</param>
        /// <param name="suma">užsakytų prekių suma</param>
        /// <param name="darbuotojas">pardavėjo vardas ir pavardė</param>
        /// <param name="data">užsakymo pateikimo data</param>
        /// <param name="pidkiekis">užsakytų prekių id ir kiekiai</param>
        public static void Naujas_Užsakymas(string id, string klientas, double suma, string darbuotojas, DateTime data, string[] pid, short[] kiekis)
        {
            //List'ų papildymas
            Užsakymas užs = new Užsakymas(id, klientas, suma, darbuotojas, data);
            Detalizacija det = new Detalizacija(id, pid, kiekis);
            Užsakymai.Add(užs);
            Detalės.Add(det);


            //Text failo papildymas
            using (var fr = new StreamWriter(File.Open(UžDataFile, FileMode.Append), Encoding.GetEncoding(1257)))
            {
                fr.WriteLine(užs.ToString());
            }

            using (var fr = new StreamWriter(File.Open(DeDataFile, FileMode.Append), Encoding.GetEncoding(1257)))
            {
                fr.WriteLine(det.ToString());
            }
        }

        /// <summary>
        /// Sukuria nauja kliento elementa
        /// </summary>
        /// <param name="id">kliento id</param>
        /// <param name="tipas">kliento tipas (fizinis, juridinis)</param>
        /// <param name="pavadinimas">Kliento pavadinimas / vardas pavarde</param>
        /// <param name="kodas">kliento kodas</param>
        /// <param name="telNr">kliento telefoo numeris</param>
        public static void Naujas_Klientas(string id, string tipas, string pavadinimas, long kodas, long telNr)
        {
            //List'o papildymas
            Klientas klientas = new Klientas(id, tipas, pavadinimas, kodas, telNr);
            Klientai.Add(klientas);

            //Text failo papildymas
            using (var fr = new StreamWriter(File.Open(KlDataFile, FileMode.Append), Encoding.GetEncoding(1257)))
            {
                fr.WriteLine(klientas.ToString());
            }
        }

        /// <summary>
        /// Naujas Darbuotojas
        /// </summary>
        /// <param name="tabelis"></param>
        /// <param name="varpav"></param>
        public static void Naujas_Darbuotojas(string tabelis, string varpav)
        {
            //List'o papildymas
            Pardavėjas pardavėjas = new Pardavėjas(tabelis, varpav);
            Pardavėjai.Add(pardavėjas);

            //Text failo papildymas
            using (var fr = new StreamWriter(File.Open(DaDataFile, FileMode.Append), Encoding.GetEncoding(1257)))
            {
                fr.WriteLine(pardavėjas.ToString());
            }
        }

        /// <summary>
        /// Sukuria nauja preke
        /// </summary>
        /// <param name="tabelis">prekes id</param>
        /// <param name="pav">prekes pavadinimas</param>
        /// <param name="kaina">prekes kaina</param>
        public static void Nauja_Prek(string tabelis, string pav, double kaina)
        {
            //List'o papildymas
            Prekė prekė = new Prekė(tabelis, pav, kaina);
            Prekės.Add(prekė);

            //Text failo papildymas
            using (var fr = new StreamWriter(File.Open(PDataFile, FileMode.Append), Encoding.GetEncoding(1257)))
            {
                fr.WriteLine(prekė.ToString());
            }
        }
        
        /// <summary>
        /// Iraso nauja elute i faila
        /// </summary>
        /// <param name="elementas">elementas kuri irasys i faila</param>
        /// <param name="nr">failo numeris failu masyve</param>
        public static void Salinimas(Object elementas, int nr)
        {            
            using (var fr = new StreamWriter(File.Open(failai[nr], FileMode.Append), Encoding.GetEncoding(1257)))
            {
                fr.WriteLine(elementas.ToString());
            }
        }

    }
}

