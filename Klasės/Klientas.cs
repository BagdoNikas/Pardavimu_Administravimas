using System;


namespace Administravimas
{
    /// <summary>
    /// Klasė duomenims apie klientą saugoti
    /// </summary>
    public class Klientas
    {
        public string ID { get; set; }  // Kliento ID kodas
        public string Tipas { get; set; }   // Kliento tipas
        public string Pavadinimas { get; set; } // Kliento įmonės pavadinimas/vardas pavardė
        public long Kodas { get; set; } // Kliento įmonės kodas/asmens kodas
        public long TelNr { get; set; } // kliento telefono numeris

        /// <summary>
        /// Tuščias konstruktorius
        /// </summary>
        public Klientas() { }

        /// <summary>
        /// Konstruktorius kliento duomenims užpildyti
        /// </summary>
        /// <param name="id">id kodas</param>
        /// <param name="tipas">asmens tipas</param>
        /// <param name="pavadinimas">pavadinimas</param>
        /// <param name="kodas">kodas</param>
        /// <param name="telNr">telefono numeris</param>
        public Klientas(string id, string tipas, string pavadinimas, long kodas, long telNr)
        {
            ID = id;
            Tipas = tipas;
            Pavadinimas = pavadinimas;
            Kodas = kodas;
            TelNr = telNr;
        }

        public override string ToString()
        {
            return ID + ";" + Tipas + ";" + Pavadinimas + ";" + Kodas.ToString() + ";" + TelNr.ToString();
        }
    }
}
