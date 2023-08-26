using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace PR60PR69
{
    /// <summary>
    /// Interaction logic for DodavanjeCveta.xaml
    /// </summary>
    public partial class DodavanjeCveta : Window
    {
        public DodavanjeCveta()
        {
            InitializeComponent();
        }

        private double cena = 0;

        public double Cena
        {
            get { return cena; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cena = double.Parse(CenaBox.Text);
            } catch(Exception)
            {
                MessageBox.Show("Cena nije pravilno uneta");
            }
            finally
            {
                CenaBox.Text = "";
            }
            this.Close();
        }
    }
}
