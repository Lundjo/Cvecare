using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR60PR69
{
    public class CvetUCvecari : Cvet
    {
        private double cena;

        public CvetUCvecari(string naziv, string lat_naziv, string boja, double cena) : base(naziv, lat_naziv, boja)
        {
            this.cena = cena;
        }
        
        public CvetUCvecari(Cvet cvet, double cena):base(cvet.Naziv, cvet.LatNaziv, cvet.Boja)
        {
            this.cena = cena;
        }
        
        public double Cena
        {
            get { return cena; }
            set
            {
                if (value != cena)
                {
                    cena = value;
                    OnPropertyChanged("Cena");
                }
            }
        }
    }
}
