using System;


namespace Administravimas
{
    /// <summary>
    /// Pardavėjo informaciją aprašanti klasė
    /// </summary>
    public class Pardavėjas
    {
        public string ID { get; set; }  // Pardavėjo ID kodas
        public string VardasPavardė { get; set; }   // Pardavėjo vardas ir pavardė
                
        /// <summary>
        /// Tuščias konstruktorius
        /// </summary>
        public Pardavėjas() { }

        /// <summary>
        /// Konstruktorius pardavėjo informacijai užpildyti
        /// </summary>
        /// <param name="id">pardavėjo id</param>
        /// <param name="vardasPavardė">pardavėjo vardas ir pavardė</param>
        public Pardavėjas(string id, string vardasPavardė)
        {
            ID = id;
            VardasPavardė = vardasPavardė;
        }

        public override string ToString()
        {
            return ID + ";" + VardasPavardė;
        }
    }
}
