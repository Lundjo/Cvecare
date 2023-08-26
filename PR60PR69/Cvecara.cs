using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PR60PR69
{
    public class Cvecara : INotifyPropertyChanged
    {
        private string naziv;
        private string adresa;
        private int god_osn;
        private string logo_putanja;
        private ObservableCollection<CvetUCvecari> cvece;
        private ObservableCollection<Radnik> radnici;
        private IList<object> listaSvega;
        

        public ObservableCollection<CvetUCvecari> Cvece
        {
            get { return cvece; }
            set
            {
                if (value != cvece)
                {
                    cvece = value;
                    OnPropertyChanged("Cvece");
                }
            }
        }

        public ObservableCollection<Radnik> Radnici
        {
            get { return radnici; }
            set
            {
                if (value != radnici)
                {
                    radnici = value;
                    OnPropertyChanged("Radnici");
                }
            }
        }

        public IList<object> ListaSvega
        {
            get
            {
                IList<object> lista = new List<object>();

                if (Radnici.Count != 0)
                {
                    lista.Add("--Radnici--");
                    foreach (Radnik r in Radnici) lista.Add(r);
                }

                if (Cvece.Count != 0)
                {
                    lista.Add("--Cvece--");
                    foreach (CvetUCvecari cv in Cvece) lista.Add(cv);
                }

                return lista;
            }
            set
            {
                if (value != listaSvega)
                {
                    listaSvega = value;
                    OnPropertyChanged("ListaSvega");
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public Cvecara(string naziv, string adresa, int god_osn, string logo_putanja)
        {
            this.naziv = naziv;
            this.adresa = adresa;
            this.god_osn = god_osn;
            this.logo_putanja = logo_putanja;
            this.Cvece = new ObservableCollection<CvetUCvecari>();
            this.Radnici = new ObservableCollection<Radnik>();
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

        public string Adresa
        {
            get { return adresa; }
            set
            {
                if (value != adresa)
                {
                    adresa = value;
                    OnPropertyChanged("Adresa");
                }
            }
        }

        public int God_osn
        {
            get { return god_osn; }
            set
            {
                if (value != god_osn)
                {
                    god_osn = value;
                    OnPropertyChanged("God_osn");
                }
            }
        }

        public string Logo_putanja
        {
            get { return logo_putanja; }
            set
            {
                if (value != logo_putanja)
                {
                    logo_putanja = value;
                    OnPropertyChanged("Logo_putanja");
                }
            }
        }

        public override string ToString()
        {
            string ispis = "";
            ispis += naziv + "," + adresa + "," + god_osn.ToString() + "," + logo_putanja + "%";
            foreach (CvetUCvecari c in Cvece)
            {
                ispis+=c.LatNaziv+","+c.Cena+";";
            }
            ispis += "%";
            foreach (Radnik r in Radnici)
            {
                ispis += r.Jmbg + ";";
            }
            ispis += "%";
            return ispis;
        }
    }
}
