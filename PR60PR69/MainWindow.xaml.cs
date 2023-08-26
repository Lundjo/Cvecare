using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PR60PR69
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point startPoint = new Point();

        public MainWindow()
        {
            System.Threading.Thread.Sleep(3500);
            InitializeComponent();
            this.DataContext = this;
            Cvece1 = new ObservableCollection<Cvet>();
            Radnici1 = new ObservableCollection<Radnik>();
            Cvecare1 = new ObservableCollection<Cvecara>();
            Cvecare2 = new ObservableCollection<Cvecara>();
            Ucitaj_Iz_Fajla();
        }

        public string radnikSlikaPutanja="";
        public string cvecaraSlikaPutanja="";
        public ObservableCollection<Cvet> Cvece1
        {
            get;
            set;
        }
        public ObservableCollection<Radnik> Radnici1
        {
            get;
            set;
        }
        public ObservableCollection<Cvecara> Cvecare1
        {
            get;
            set;
        }
        public ObservableCollection<Cvecara> Cvecare2
        {
            get;
            set;
        }

        private void DodajCvet_Click(object sender, RoutedEventArgs e)
        {
            DCNaziv.Text = ""; DCLatNaziv.Text = ""; DCBoja.Text = "";
            ListaCveca.SelectedItem = ListaRadnika.SelectedItem = null;
            Cvet.Visibility = DCNaziv.Visibility = DCBoja.Visibility = DCLatNaziv.Visibility = DodavanjeCveta_Dodaj.Visibility = DodavanjeCveta_Otkazi.Visibility = (Visibility)0;
            Cena.Visibility = ICCena.Visibility = DCCena.Visibility = Cvecara.Visibility = ICNaziv.Visibility = ICLatNaziv.Visibility = ICBoja.Visibility = Promena_cveta_izmeni.Visibility = Radnik.Visibility = (Visibility)2;
            RadnjaCvetom.Text = "Dodavanje cveta";
        }
        private void DodajRadnika_Click(object sender, RoutedEventArgs e)
        {
            DRGod.Text = ""; DRIme.Text = ""; DRPrezime.Text = ""; DRJMBG.Text = ""; radnikSlikaPutanja = "";
            ListaCveca.SelectedItem = ListaRadnika.SelectedItem = null;
            Radnik.Visibility = DRIme.Visibility = DRPrezime.Visibility = DRJMBG.Visibility = DRGod.Visibility = DRSlika.Visibility = DRDodaj.Visibility = DROtkazi.Visibility = (Visibility)0;
            Cvecara.Visibility = IRIme.Visibility = IRPrezime.Visibility = IRJMBG.Visibility = IRGod.Visibility = SlikaRadnika.Visibility = PRIzmeni.Visibility = Cvet.Visibility = (Visibility)2;
            RadnjaRadnikom.Text = "Dodavanje radnika";
        }
        private void DodajCvecaru_Click(object sender, RoutedEventArgs e)
        {
            ListaRadnika.SelectedItem = ListaCveca.SelectedItem = null;
            ICrNaziv.Text = ICrAdresa.Text = ICrGodina.Text = "";
            Cvecara.Visibility = DCrNaziv.Visibility = DCrAdresa.Visibility = DCrGodina.Visibility = DCrDodaj.Visibility = DCrOtkazi.Visibility = DCrSlika.Visibility = (Visibility)0;
            DCrIzmeni.Visibility = Cvet.Visibility = Radnik.Visibility = ICrAdresa.Visibility = ICrGodina.Visibility = ICrNaziv.Visibility = (Visibility)2;
            RadnjaCvecarom.Text = "Dodavanje cvecare";
            
        }

        private void DodavanjeCveta_Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Cvet.Visibility = (Visibility)2;
        }
        private void DodavanjeCveta_Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if(DCNaziv.Text=="" || DCLatNaziv.Text=="" || DCBoja.Text == "")
            {
                MessageBox.Show("Niste uneli sve podatke");
            }
            else
            {
                Cvece1.Add(new Cvet(VelikoPrvoSlovo(DCNaziv.Text), VelikoPrvoSlovo(DCLatNaziv.Text), VelikoPrvoSlovo(DCBoja.Text)));
                DCNaziv.Text = ""; DCLatNaziv.Text = ""; DCBoja.Text = "";
                Cvet.Visibility = (Visibility)2;
            }
        }
        private void Promena_cveta_izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (ListaCveca.SelectedItem != null)
            {
                Cvet c = this.ListaCveca.SelectedItem as Cvet;
                if (DCNaziv.Text == "" || DCLatNaziv.Text == "" || DCBoja.Text == "")
                {
                    MessageBox.Show("Izmena nije dobra");
                }
                else
                {
                    c.Naziv = VelikoPrvoSlovo(DCNaziv.Text);
                    c.LatNaziv = VelikoPrvoSlovo(DCLatNaziv.Text);
                    c.Boja = VelikoPrvoSlovo(DCBoja.Text);
                    ListaCveca.SelectedItem = null;
                    DCNaziv.Text = DCLatNaziv.Text = DCBoja.Text = "";
                    Cvet.Visibility = (Visibility)2;
                }
            }
            else if ((trvCvecare.SelectedItem as Cvet) != null)
            {
                CvetUCvecari c = this.trvCvecare.SelectedItem as CvetUCvecari;
                if (DCNaziv.Text == "" || DCLatNaziv.Text == "" || DCBoja.Text == "" || DCCena == null)
                {
                    MessageBox.Show("Izmena nije dobra");
                }
                else
                {
                    c.Naziv = VelikoPrvoSlovo(DCNaziv.Text);
                    c.LatNaziv = VelikoPrvoSlovo(DCLatNaziv.Text);
                    c.Boja = VelikoPrvoSlovo(DCBoja.Text);
                    c.Cena = double.Parse(DCCena.Text);
                    ListaCveca.SelectedItem = null;
                    DCNaziv.Text = DCLatNaziv.Text = DCBoja.Text = "";
                    Cvet.Visibility = (Visibility)2;
                }
            }
        }

        private void DRSlika_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog otvaranjeFajla = new OpenFileDialog();
            otvaranjeFajla.Filter = "Slike (*.png;*.jpeg;*.bmp;*.jpg;)|*.png;*.jpeg;*.bmp;*.jpg;";
            if (otvaranjeFajla.ShowDialog() == true)
            {
                string putanja = otvaranjeFajla.FileName;
                radnikSlikaPutanja = putanja;
            }
        }
        private void DROtkazi_Click(object sender, RoutedEventArgs e)
        {
            Radnik.Visibility = (Visibility)2;
        }
        private void DRDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (DRGod.Text == "" || DRIme.Text=="" || DRPrezime.Text=="" || DRJMBG.Text=="" || radnikSlikaPutanja == "")
            {
                MessageBox.Show("Niste uneli sve podatke");
            } else if (!ProveriJMBG(DRJMBG.Text))
            {
                MessageBox.Show("JMBG nije pravilno unet. Mora sadrzati samo cifre i treba da ih bude 13");
            } else
            {
                try
                {
                    Radnici1.Add(new Radnik(radnikSlikaPutanja, VelikoPrvoSlovo(DRIme.Text), VelikoPrvoSlovo(DRPrezime.Text), DRJMBG.Text, int.Parse(DRGod.Text)));
                }
                catch(Exception izuzetak)
                {
                    MessageBox.Show(izuzetak.Message);
                }
                finally
                {
                    DRGod.Text = ""; DRIme.Text = ""; DRPrezime.Text = ""; DRJMBG.Text = ""; radnikSlikaPutanja = "";
                    Radnik.Visibility = SlikaRadnika.Visibility = (Visibility)2;
                }
            }
        }
        private void PRIzmeni_Click(object sender, RoutedEventArgs e)
        {
            /*Radnik r = this.ListaRadnika.SelectedItem as Radnik;
            if (DRGod.Text == "" || DRIme.Text == "" || DRPrezime.Text == "" || DRJMBG.Text == "" || radnikSlikaPutanja == "")
            {
                MessageBox.Show("Nije ispravan unos");
            }
            else if (!ProveriJMBG(DRJMBG.Text))
            {
                MessageBox.Show("JMBG nije pravilno unet. Mora sadrzati samo cifre i treba da ih bude 13");
            }
            else
            {
                try
                {
                    r.Slika_putanja = radnikSlikaPutanja;
                    r.Ime = VelikoPrvoSlovo(DRIme.Text);
                    r.Prezime = VelikoPrvoSlovo(DRPrezime.Text);
                    r.Jmbg = DRJMBG.Text;
                    r.God_iskustva = int.Parse(DRGod.Text);
                }
                catch(Exception izuzetak)
                {
                    MessageBox.Show(izuzetak.Message);
                }
                finally
                {
                    DRGod.Text = DRIme.Text = DRPrezime.Text = DRJMBG.Text = radnikSlikaPutanja = "";
                }
                ListaRadnika.SelectedItem = null;
                Radnik.Visibility = SlikaRadnika.Visibility = (Visibility)2;
            }*/
            if(ListaRadnika.SelectedItem != null)
            {
                Radnik r = this.ListaRadnika.SelectedItem as Radnik;
                if (DRGod.Text == "" || DRIme.Text == "" || DRPrezime.Text == "" || DRJMBG.Text == "" || radnikSlikaPutanja == "")
                {
                    MessageBox.Show("Nije ispravan unos");
                }
                else if (!ProveriJMBG(DRJMBG.Text))
                {
                    MessageBox.Show("JMBG nije pravilno unet. Mora sadrzati samo cifre i treba da ih bude 13");
                }
                else
                {
                    try
                    {
                        r.Slika_putanja = radnikSlikaPutanja;
                        r.Ime = VelikoPrvoSlovo(DRIme.Text);
                        r.Prezime = VelikoPrvoSlovo(DRPrezime.Text);
                        r.Jmbg = DRJMBG.Text;
                        r.God_iskustva = int.Parse(DRGod.Text);
                    }
                    catch (Exception izuzetak)
                    {
                        MessageBox.Show(izuzetak.Message);
                    }
                    finally
                    {
                        DRGod.Text = DRIme.Text = DRPrezime.Text = DRJMBG.Text = radnikSlikaPutanja = "";
                    }
                    ListaRadnika.SelectedItem = null;
                    Radnik.Visibility = SlikaRadnika.Visibility = (Visibility)2;
                }
            } 
            else if((trvCvecare.SelectedItem as Radnik) != null)
            {
                Radnik r = this.trvCvecare.SelectedItem as Radnik;
                if (DRGod.Text == "" || DRIme.Text == "" || DRPrezime.Text == "" || DRJMBG.Text == "" || radnikSlikaPutanja == "")
                {
                    MessageBox.Show("Nije ispravan unos");
                }
                else if (!ProveriJMBG(DRJMBG.Text))
                {
                    MessageBox.Show("JMBG nije pravilno unet. Mora sadrzati samo cifre i treba da ih bude 13");
                }
                else
                {
                    try
                    {
                        r.Slika_putanja = radnikSlikaPutanja;
                        r.Ime = VelikoPrvoSlovo(DRIme.Text);
                        r.Prezime = VelikoPrvoSlovo(DRPrezime.Text);
                        r.Jmbg = DRJMBG.Text;
                        r.God_iskustva = int.Parse(DRGod.Text);
                    }
                    catch (Exception izuzetak)
                    {
                        MessageBox.Show(izuzetak.Message);
                    }
                    finally
                    {
                        DRGod.Text = DRIme.Text = DRPrezime.Text = DRJMBG.Text = radnikSlikaPutanja = "";
                    }
                    ListaRadnika.SelectedItem = null;
                    Radnik.Visibility = SlikaRadnika.Visibility = (Visibility)2;
                }
            }
        }

        public string VelikoPrvoSlovo(string ulaz)
        {
            char[] izlaz;
            izlaz = ulaz.ToLower().ToCharArray();
            izlaz[0]=char.ToUpper(izlaz[0]);
            return new string(izlaz);
        }
        public bool ProveriJMBG(string jmbg)
        {
            if (jmbg.Length != 13)
            {
                return false;
            }
            foreach (char c in jmbg)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }

        private void ListaCveca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cvet.Visibility = ICNaziv.Visibility = ICLatNaziv.Visibility = ICBoja.Visibility = (Visibility)0;
            Cena.Visibility = ICCena.Visibility = DCCena.Visibility = Cvecara.Visibility = DCNaziv.Visibility = DCBoja.Visibility = DCLatNaziv.Visibility = DodavanjeCveta_Dodaj.Visibility = DodavanjeCveta_Otkazi.Visibility = Promena_cveta_izmeni.Visibility = Radnik.Visibility = (Visibility)2;
            RadnjaCvetom.Text = "Informacije o cvetu";
            Cvet cvet=this.ListaCveca.SelectedItem as Cvet;
            if (cvet != null)
            {
                ICNaziv.Text = cvet.Naziv;
                ICLatNaziv.Text = cvet.LatNaziv;
                ICBoja.Text = cvet.Boja;
            }
            ListaRadnika.SelectedItem = null;
        }
        private void ListaRadnika_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Radnik.Visibility = IRIme.Visibility = IRPrezime.Visibility = IRJMBG.Visibility = IRGod.Visibility = SlikaRadnika.Visibility = (Visibility)0;
            Cvecara.Visibility = DRIme.Visibility = DRPrezime.Visibility = DRJMBG.Visibility = DRGod.Visibility = DRSlika.Visibility = DRDodaj.Visibility = DROtkazi.Visibility = PRIzmeni.Visibility = Cvet.Visibility = (Visibility)2;
            RadnjaRadnikom.Text = "Informacije o radniku";
            Radnik radnik = this.ListaRadnika.SelectedItem as Radnik;
            if (radnik != null)
            {
                try
                {
                    SlikaRadnika.Source = new BitmapImage(new Uri(radnik.Slika_putanja));
                }
                catch (Exception izuzetak)
                {
                    MessageBox.Show("Doslo je do greske prilikom ucitavanja slike: " + izuzetak.Message);
                }
                finally
                {
                    IRIme.Text = radnik.Ime;
                    IRPrezime.Text = radnik.Prezime;
                    IRJMBG.Text = radnik.Jmbg;
                    IRGod.Text = radnik.God_iskustva.ToString();
                }
            }
            ListaCveca.SelectedItem = null;
        }
        private void TrvCvecare_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ListaCveca.SelectedItem = ListaRadnika.SelectedItem = null;
            Radnik.Visibility = (Visibility)2;
            Cvecara cv = null;
            Radnik r = null;
            CvetUCvecari c = null;
            if ((trvCvecare.SelectedItem as Cvecara) != null)
                cv = trvCvecare.SelectedItem as Cvecara;
            else if ((trvCvecare.SelectedItem as Radnik) != null)
                r = trvCvecare.SelectedItem as Radnik;
            else if ((trvCvecare.SelectedItem as CvetUCvecari) != null)
                c = trvCvecare.SelectedItem as CvetUCvecari;    

            if (cv != null)
            {
                DCrIzmeni.Visibility = Cvet.Visibility = Radnik.Visibility = DCrAdresa.Visibility = DCrDodaj.Visibility = DCrGodina.Visibility = DCrNaziv.Visibility = DCrOtkazi.Visibility = DCrSlika.Visibility = (Visibility)2;
                Cvecara.Visibility = ICrAdresa.Visibility = ICrGodina.Visibility = ICrNaziv.Visibility = (Visibility)0;
                RadnjaRadnikom.Text = "Informacije o cvecari";
                ICrNaziv.Text = cv.Naziv;
                ICrAdresa.Text = cv.Adresa;
                ICrGodina.Text = cv.God_osn.ToString();
            }
            else if(r != null)
            {
                Radnik.Visibility = IRIme.Visibility = IRPrezime.Visibility = IRJMBG.Visibility = IRGod.Visibility = SlikaRadnika.Visibility = (Visibility)0;
                Cvecara.Visibility = DRIme.Visibility = DRPrezime.Visibility = DRJMBG.Visibility = DRGod.Visibility = DRSlika.Visibility = DRDodaj.Visibility = DROtkazi.Visibility = PRIzmeni.Visibility = Cvet.Visibility = (Visibility)2;
                RadnjaRadnikom.Text = "Informacije o radniku";
                radnikSlikaPutanja = r.Slika_putanja;
                IRIme.Text = r.Ime;
                IRPrezime.Text = r.Prezime;
                IRJMBG.Text = r.Jmbg;
                IRGod.Text = r.God_iskustva.ToString();
            }
            else if(c != null)
            {
                Cena.Visibility = ICCena.Visibility = Cvet.Visibility = ICNaziv.Visibility = ICLatNaziv.Visibility = ICBoja.Visibility = (Visibility)0;
                DCCena.Visibility = Cvecara.Visibility = DCNaziv.Visibility = DCBoja.Visibility = DCLatNaziv.Visibility = DodavanjeCveta_Dodaj.Visibility = DodavanjeCveta_Otkazi.Visibility = Promena_cveta_izmeni.Visibility = Radnik.Visibility = (Visibility)2;
                RadnjaCvetom.Text = "Informacije o cvetu";
                ICNaziv.Text = c.Naziv;
                ICLatNaziv.Text = c.LatNaziv;
                ICBoja.Text = c.Boja;
                ICCena.Text = c.Cena.ToString();
            }
        }

        private void Promeni_radnika_Click(object sender, RoutedEventArgs e)
        {
            Radnik.Visibility = DRIme.Visibility = DRPrezime.Visibility = DRJMBG.Visibility = DRGod.Visibility = DRSlika.Visibility = PRIzmeni.Visibility = DROtkazi.Visibility = (Visibility)0;
            Cvecara.Visibility = IRIme.Visibility = IRPrezime.Visibility = IRJMBG.Visibility = IRGod.Visibility = SlikaRadnika.Visibility = DRDodaj.Visibility = Cvet.Visibility = (Visibility)2;
            RadnjaRadnikom.Text = "Izmena radnika";
            /*Radnik r = this.ListaRadnika.SelectedItem as Radnik;
            if (r != null)
            {
                radnikSlikaPutanja = r.Slika_putanja;
                DRIme.Text = r.Ime;
                DRPrezime.Text = r.Prezime;
                DRJMBG.Text = r.Jmbg;
                DRGod.Text = r.God_iskustva.ToString();
            }*/
            if(ListaRadnika.SelectedItem != null)
            {
                Radnik r = this.ListaRadnika.SelectedItem as Radnik;
                radnikSlikaPutanja = r.Slika_putanja;
                DRIme.Text = r.Ime;
                DRPrezime.Text = r.Prezime;
                DRJMBG.Text = r.Jmbg;
                DRGod.Text = r.God_iskustva.ToString();
            }
            else if((trvCvecare.SelectedItem as Radnik) != null)
            {
                Radnik r = this.trvCvecare.SelectedItem as Radnik;
                radnikSlikaPutanja = r.Slika_putanja;
                DRIme.Text = r.Ime;
                DRPrezime.Text = r.Prezime;
                DRJMBG.Text = r.Jmbg;
                DRGod.Text = r.God_iskustva.ToString();
            }
        }
        private void Promeni_cvet_Click(object sender, RoutedEventArgs e)
        {
            Cvet.Visibility = DCNaziv.Visibility = DCBoja.Visibility = DCLatNaziv.Visibility = DodavanjeCveta_Otkazi.Visibility = Promena_cveta_izmeni.Visibility = (Visibility)0;
            Cena.Visibility = ICCena.Visibility = DCCena.Visibility = Cvecara.Visibility = ICNaziv.Visibility = ICLatNaziv.Visibility = ICBoja.Visibility = DodavanjeCveta_Dodaj.Visibility = Radnik.Visibility = (Visibility)2;
            RadnjaCvetom.Text = "Izmena cveta";
            if(ListaCveca.SelectedItem != null)
            {
                Cvet c = this.ListaCveca.SelectedItem as Cvet;
                DCNaziv.Text = c.Naziv;
                DCLatNaziv.Text = c.LatNaziv;
                DCBoja.Text = c.Boja;
            }
            else if((trvCvecare.SelectedItem as Cvet) != null)
            {
                CvetUCvecari c = this.trvCvecare.SelectedItem as CvetUCvecari;
                DCNaziv.Text = c.Naziv;
                DCLatNaziv.Text = c.LatNaziv;
                DCBoja.Text = c.Boja;
                DCCena.Text = c.Cena.ToString();
            }
        }
        private void Obrisi_radnika_Click(object sender, RoutedEventArgs e)
        {
            Radnik r = this.ListaRadnika.SelectedItem as Radnik;
            Radnici1.Remove(r);
            Radnik.Visibility = (Visibility)2;
        }
        private void Obrisi_cvet_Click(object sender, RoutedEventArgs e)
        {
            Cvet c = this.ListaCveca.SelectedItem as Cvet;
            Cvece1.Remove(c);
            Cvet.Visibility = (Visibility)2;
        }
        private void Promeni_cvecaru_Click(object sender, RoutedEventArgs e)
        {
            Cvecara cv = null;
            Radnik r = null;
            CvetUCvecari c = null;
            if ((trvCvecare.SelectedItem as Cvecara) != null)
                cv = trvCvecare.SelectedItem as Cvecara;
            else if ((trvCvecare.SelectedItem as Radnik) != null)
                r = trvCvecare.SelectedItem as Radnik;
            else if ((trvCvecare.SelectedItem as CvetUCvecari) != null)
                c = trvCvecare.SelectedItem as CvetUCvecari;
            if(cv != null)
            {
                Cvet.Visibility = Radnik.Visibility = DCrDodaj.Visibility = ICrAdresa.Visibility = ICrGodina.Visibility = ICrNaziv.Visibility = (Visibility)2;
                Cvecara.Visibility = DCrAdresa.Visibility = DCrGodina.Visibility = DCrNaziv.Visibility = DCrSlika.Visibility = DCrOtkazi.Visibility = DCrIzmeni.Visibility = (Visibility)0;
                RadnjaCvecarom.Text = "Izmena cvecare";
                DCrNaziv.Text = cv.Naziv;
                DCrAdresa.Text = cv.Adresa;
                DCrGodina.Text = cv.God_osn.ToString();
            } else if(r != null)
            {
                Radnik.Visibility = DRIme.Visibility = DRPrezime.Visibility = DRJMBG.Visibility = DRGod.Visibility = DRSlika.Visibility = PRIzmeni.Visibility = DROtkazi.Visibility = (Visibility)0;
                Cvecara.Visibility = IRIme.Visibility = IRPrezime.Visibility = IRJMBG.Visibility = IRGod.Visibility = SlikaRadnika.Visibility = DRDodaj.Visibility = Cvet.Visibility = (Visibility)2;
                RadnjaRadnikom.Text = "Izmena radnika";
                radnikSlikaPutanja = r.Slika_putanja;
                DRIme.Text = r.Ime;
                DRPrezime.Text = r.Prezime;
                DRJMBG.Text = r.Jmbg;
                DRGod.Text = r.God_iskustva.ToString();
            } else if(c != null)
            {
                Cena.Visibility = DCCena.Visibility = Cvet.Visibility = DCNaziv.Visibility = DCBoja.Visibility = DCLatNaziv.Visibility = DodavanjeCveta_Otkazi.Visibility = Promena_cveta_izmeni.Visibility = (Visibility)0;
                ICCena.Visibility = Cvecara.Visibility = ICNaziv.Visibility = ICLatNaziv.Visibility = ICBoja.Visibility = DodavanjeCveta_Dodaj.Visibility = Radnik.Visibility = (Visibility)2;
                RadnjaCvetom.Text = "Izmena cveta"; 
                DCNaziv.Text = c.Naziv;
                DCLatNaziv.Text = c.LatNaziv;
                DCBoja.Text = c.Boja;
                DCCena.Text = c.Cena.ToString();
            }
        }
        private void Obrisi_cvecaru_Click(object sender, RoutedEventArgs e)
        {
            Cvecara cv = trvCvecare.SelectedItem as Cvecara;
            if (cv != null)
            {
                Cvecare1.Remove(cv);
            }
            Cvecara.Visibility = (Visibility)2;
            /*Cvecara cv = trvCvecare.SelectedItem as Cvecara;
            if ((trvCvecare.SelectedItem as Cvecara) != null)
                Cvecare1.Remove(cv);
            else if ((trvCvecare.SelectedItem as Radnik) != null)
                cv.Radnici.Remove(trvCvecare.SelectedItem as Radnik);*/
        }

        private void DCrSlika_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog otvaranjeFajla = new OpenFileDialog();
            otvaranjeFajla.Filter = "Slike (*.png;*.jpeg;*.bmp;*.jpg;)|*.png;*.jpeg;*.bmp;*.jpg;";
            if (otvaranjeFajla.ShowDialog() == true)
            {
                string putanja = otvaranjeFajla.FileName;
                cvecaraSlikaPutanja = (ImageSource)putanja;
            }
        }
        private void DCrDodaj_Click(object sender, RoutedEventArgs e)
        {
            if(DCrNaziv.Text=="" || DCrAdresa.Text=="" || DCrGodina.Text=="" || cvecaraSlikaPutanja == "")
            {
                MessageBox.Show("Niste uneli sve podatke");
            } else
            {
                try
                {
                    Cvecare1.Add(new Cvecara(VelikoPrvoSlovo(DCrNaziv.Text), DCrAdresa.Text, int.Parse(DCrGodina.Text), cvecaraSlikaPutanja));
                }
                catch (Exception izuzetak)
                {
                    MessageBox.Show(izuzetak.Message);
                }
                finally
                {
                    DCrNaziv.Text = DCrAdresa.Text = DCrGodina.Text = cvecaraSlikaPutanja = "";
                }
            }
        }
        private void DCrIzmeni_Click(object sender, RoutedEventArgs e)
        {
            Cvecara cv = trvCvecare.SelectedItem as Cvecara;
            if (DCrNaziv.Text == "" || DCrAdresa.Text == "" || DCrGodina.Text == "" || cvecaraSlikaPutanja == "")
            {
                MessageBox.Show("Niste uneli sve podatke");
            }
            else
            {
                try
                {
                    cv.Naziv = VelikoPrvoSlovo(DCrNaziv.Text);
                    cv.Adresa = VelikoPrvoSlovo(DCrAdresa.Text);
                    cv.God_osn = int.Parse(DCrGodina.Text);

                }
                catch (Exception izuzetak)
                {
                    MessageBox.Show(izuzetak.Message);
                }
                finally
                {
                    DCrNaziv.Text = DCrAdresa.Text = DCrGodina.Text = cvecaraSlikaPutanja = "";
                }
                Cvecara.Visibility = (Visibility)2;
            }
        }
        private void DCrOtkazi_Click(object sender, RoutedEventArgs e)
        {
            Cvecara.Visibility=(Visibility)2;
        }

        private void Cuvanje_Click(object sender, RoutedEventArgs e)
        {
            string upis="";
            foreach (Cvet c in Cvece1)
            {
                upis += c.ToString();
            }
            string fajl = "Cvece.txt";
            string putanja = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"..//..//FAJLOVI//", fajl));
            StreamWriter sw = new StreamWriter(putanja);
            sw.Write(upis);
            sw.Close();
            upis = "";
            foreach(Radnik r in Radnici1)
            {
                upis += r.ToString();
            }
            fajl = "Radnici.txt";
            putanja = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"..//..//FAJLOVI//", fajl));
            sw = new StreamWriter(putanja);
            sw.Write(upis);
            sw.Close();
            upis = "";
            fajl = "Cvecare.txt";
            putanja = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"..//..//FAJLOVI//", fajl));
            foreach(Cvecara c in Cvecare1)
            {
                upis += c.ToString()+"`";
            }
            sw = new StreamWriter(putanja);
            sw.Write(upis);
            sw.Close();
            ListaCveca.SelectedItem = ListaRadnika.SelectedItem = null;
            Radnik.Visibility = Cvet.Visibility = (Visibility)2;
        }
        private void Ucitaj_Iz_Fajla()
        {
            string fajl = "Cvece.txt";
            string putanja = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"..//..//FAJLOVI//", fajl));
            string ucitano = System.IO.File.ReadAllText(putanja);
            string[] cvece =ucitano.Split(';');
            foreach (string c in cvece)
            {
                if (c != "")
                {
                    string[] osobine = c.Split(',');
                    Cvece1.Add(new Cvet(osobine[0], osobine[1], osobine[2]));
                }
            }
            fajl = "Radnici.txt";
            putanja = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"..//..//FAJLOVI//", fajl));
            ucitano = System.IO.File.ReadAllText(putanja);
            string[] radnici = ucitano.Split(';');
            foreach(string r in radnici)
            {
                if (r!="")
                {
                    string[] osobine = r.Split(',');
                    Radnici1.Add(new Radnik(osobine[0], osobine[1], osobine[2], osobine[3], int.Parse(osobine[4])));
                }
            }
            fajl = "Cvecare.txt";
            putanja = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"..//..//FAJLOVI//", fajl));
            ucitano = System.IO.File.ReadAllText(putanja);
            string[] cvecare = ucitano.Split('`');
            foreach(string cvr in cvecare)
            {
                if (cvr != "")
                {
                    int i = 0;
                    string[] osobine = cvr.Split('%');
                    string[] osnovno = osobine[0].Split(',');
                    Cvecare1.Add(new Cvecara(osnovno[0], osnovno[1], int.Parse(osnovno[2]), osnovno[3]));
                    string[] cvece1 = osobine[1].Split(';');
                    foreach (string cvet in cvece1)
                    {
                        if (cvet != "")
                        {
                            string[] osobineCveta = cvet.Split(',');
                            foreach (Cvet c in Cvece1)
                            {
                                if (c.LatNaziv == osobineCveta[0])
                                {
                                    Cvecare1.ElementAt(i).Cvece.Add(new CvetUCvecari(c, double.Parse(osobineCveta[1])));
                                }
                            }
                        }
                    }
                    string[] radnici1 = osobine[2].Split(';');
                    foreach (string radnik in radnici1)
                    {
                        if (radnik != "")
                        {
                            foreach (Radnik r in Radnici1)
                            {
                                if (r.Jmbg==radnik)
                                {
                                    Cvecare1.ElementAt(i).Radnici.Add(r);
                                }
                            }
                        }
                    }
                    ++i;
                }
            }
        }

        private void Pretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Pretraga.Text != "")
            {
                trvCvecare.ItemsSource = Cvecare2;
                Cvecare2.Clear();
                string unos = Pretraga.Text.ToUpper();
                foreach (Cvecara cvr in Cvecare1)
                {
                    if (cvr.Naziv.ToUpper().Contains(unos))
                    {
                        Cvecare2.Add(cvr);
                    }
                }
            }
            else
            {
                trvCvecare.ItemsSource = Cvecare1;
            }
        }
        private void Sortiraj_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < Cvecare1.Count; ++i)
            {
                double vrednost = 0;
                foreach(CvetUCvecari c in Cvecare1.ElementAt(i).Cvece)
                {
                    vrednost += c.Cena;
                }
                for(int j = i + 1; j < Cvecare1.Count; ++j)
                {
                    double vrednost2 = 0;
                    foreach(CvetUCvecari c in Cvecare1.ElementAt(j).Cvece)
                    {
                        vrednost2 += c.Cena;
                    }
                    if (vrednost < vrednost2)
                    {
                        Cvecara c1 = Cvecare1.ElementAt(i);
                        Cvecara c2 = Cvecare1.ElementAt(j);
                        Cvecare1.RemoveAt(i);
                        Cvecare1.Insert(i, c2);
                        Cvecare1.RemoveAt(j);
                        Cvecare1.Insert(j, c1);
                    }
                }
            }
            Cvecare2.Clear();
            foreach (Cvecara cvr in Cvecare1)
            {
                Cvecare2.Add(cvr);
            }
            ListaCveca.SelectedItem = ListaRadnika.SelectedItem = null;
            Radnik.Visibility = Cvet.Visibility = (Visibility)2;
        }

        private new void PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }
        private void ListaRadnika_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (listViewItem != null)
                {
                    Radnik radnik = (Radnik)listView.ItemContainerGenerator.
                        ItemFromContainer(listViewItem);

                    DataObject dragData = new DataObject("myFormat", radnik);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                }
            }
        }
        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void TrvCvecare_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void TrvCvecare_Drop(object sender, DragEventArgs e)
        {
            Cvecara cv = trvCvecare.SelectedItem as Cvecara;
            if (cv!=null)
            {
                var ob = e.Data.GetData("myFormat");
                if(ob is Radnik)
                {
                    cv.Radnici.Add(ob as Radnik);
                    //Radnici1.Remove(ob as Radnik);
                } else if(ob is Cvet)
                {
                    dodajCvetUCvecaru(ref cv, (ob as Cvet));
                }
                trvCvecare.ItemsSource = null;
                trvCvecare.ItemsSource = Cvecare1;
            }
        }

        public void dodajCvetUCvecaru(ref Cvecara cv, Cvet cvet)
        {
            double cena;
            DodavanjeCveta dodavanjeCveta = new DodavanjeCveta();
            if (dodavanjeCveta.ShowDialog() == true)
            {
                cena = dodavanjeCveta.Cena;
                CvetUCvecari cvc = new CvetUCvecari(cvet, cena);
                cv.Cvece.Add(cvc);
            }
            else
            {
                cena = dodavanjeCveta.Cena;
                CvetUCvecari cvc = new CvetUCvecari(cvet, cena);
                cv.Cvece.Add(cvc);
            }
        }

        private void ListaCveca_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (listViewItem != null)
                {
                    Cvet cvet = (Cvet)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);

                    DataObject dragData = new DataObject("myFormat", cvet);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                }
            }
        }
    }
}
