using System;


namespace Administravimas
{
    /// <summary>
    /// Prekės informaciją aprašanti klasė
    /// </summary>
    public class Prekė 
    {
        public string ID { get; set; }         // Prekės ID kodas
        public string Pavadinimas { get; set; }    // Prekės pavadinimas
        public double Kaina { get; set; }       // Prekės kaina

        /// <summary>
        /// Tuščias konstruktorius
        /// </summary>
        public Prekė() { }

        /// <summary>
        /// Konstruktorius prekės informacijai užpildyti
        /// </summary>
        /// <param name="id">prekės ID</param>
        /// <param name="pav">prekės pavadinimas</param>
        /// <param name="kaina">prekės kaina</param>
        public Prekė(string id, string pavadinimas, double kaina)
        {
            this.ID = id;
            this.Pavadinimas = pavadinimas;
            this.Kaina = kaina;
        }
        public override string ToString()
        {
            return ID + ";" + Pavadinimas + ";" + Kaina.ToString();
        }
    }
}
