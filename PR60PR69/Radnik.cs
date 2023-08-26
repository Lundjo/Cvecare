using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR60PR69
{
    public class Radnik : INotifyPropertyChanged
    {
        private string slika_putanja;
        private string ime;
        private string prezime;
        private string jmbg;
        private int god_iskustva;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public Radnik(string slika_putanja, string ime, string prezime, string jmbg, int god_iskustva)
        {
            this.slika_putanja = slika_putanja;
            this.ime = ime;
            this.prezime = prezime;
            this.jmbg = jmbg;
            this.god_iskustva = god_iskustva;
        }

        public string Slika_putanja
        {
            get { return slika_putanja; }
            set
            {
                if (slika_putanja != value)
                {
                    slika_putanja = value;
                    OnPropertyChanged("Slika_putanja");
                }
            }
        }

        public string Ime
        {
            get { return ime; }
            set
            {
                if (ime != value)
                {
                    ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }

        public string Prezime
        {
            get { return prezime; }
            set
            {
                if (prezime != value)
                {
                    prezime = value;
                    OnPropertyChanged("Prezime");
                }
            }
        }

        public string Jmbg
        {
            get { return jmbg; }
            set
            {
                if (jmbg != value)
                {
                    jmbg = value;
                    OnPropertyChanged("Jmbg");
                }
            }
        }

        public int God_iskustva
        {
            get { return god_iskustva; }
            set
            {
                if (god_iskustva != value)
                {
                    god_iskustva = value;
                    OnPropertyChanged("God_iskustva");
                }
            }
        }

        public override string ToString()
        {
            return slika_putanja+","+ime+","+prezime+","+jmbg+","+god_iskustva.ToString()+";";
        }
    }
}
