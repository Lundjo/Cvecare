using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR60PR69
{
    public class Cvet : INotifyPropertyChanged
    {
        private string naziv;
        private string lat_naziv;
        private string boja;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Cvet(string naziv, string lat_naziv, string boja)
        {
            this.naziv = naziv;
            this.lat_naziv = lat_naziv;
            this.boja = boja;
        }

        public string Naziv
        {
            get { return naziv; }
            set
            {
                if (value != naziv)
                {
                    naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }

        public string LatNaziv
        {
            get { return lat_naziv; }
            set
            {
                if (value != lat_naziv)
                {
                    lat_naziv = value;
                    OnPropertyChanged("LatNaziv");
                }
            }
        }

        public string Boja
        {
            get { return boja; }
            set
            {
                if (value != boja)
                {
                    boja = value;
                    OnPropertyChanged("Boja");
                }
            }
        }

        public override string ToString()
        {
            return naziv+ ","+lat_naziv+ ","+boja+";";
        }
    }
}
