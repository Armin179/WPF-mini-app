using System;
using System.Collections.Generic;
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

namespace Zadatak_2
{
        public class opstina
        {
            public int broj { get; set; }
            public string naziv { get; set; }
            public int brstan { get; set; }
        }
    public partial class MainWindow : Window
    {
        List <opstina> opstine = new List<opstina>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            string posBroj = korisnikPostanskiBroj.Text;
            string nazivOps = nazivOpstine.Text;
            string brStanovnika = brStan.Text;

            if(string.IsNullOrWhiteSpace(posBroj) || string.IsNullOrWhiteSpace(nazivOps) || string.IsNullOrWhiteSpace(brStanovnika))
            {
                if(string.IsNullOrWhiteSpace(posBroj))
                {
                    MessageBox.Show("Unesite postanski broj!");
                }
                if(string.IsNullOrWhiteSpace(nazivOps))
                {
                    MessageBox.Show("Unesite naziv opstine!");
                }
                if(string.IsNullOrWhiteSpace(brStanovnika))
                {
                    MessageBox.Show("Unesite broj stanovnika!");
                }
            }
        else
            {

                //string provPB = pb.Text.Trim(); // u proverenu promenljivu provPB smesta se tekst iz textboxa ali se poziva trim da ukloni razmake s pocetka i kraja
                //ListViewItem ProveraPB = listaOpstina.(provPB); //proverava da li moze da nadje takav postanski broj u listView-u 
                //string provNM = no.Text.Trim(); // isto radi za ime
                //ListViewItem ProveraNM = listaOpstina.FindItemWithText(provNM);
                //if (ProveraPB == null && ProveraNM == null) // proverava da li je nasao da se vec pojavio postanski broj ili ime
                //{ // ako nije nasao da se pojavljuju onda unosi novi red u listView
                //    var red = new string[] { posbroj, naziv, broj };
                //    var red1 = new ListViewItem(red);
                //    listaOpstina.Items.Add(red1);
                //    IzracunajProsek(); // racuna novi prosek
                //}
                //else
                //{ // ako je nasao onda ispisuje poruke sta od unetih vrednosti vec posotji ime ili postanski broj
                //    if (ProveraPB != null)
                //        MessageBox.Show("POSTANSKI BROJ JE VEC UNET!");
                //    if (ProveraNM != null)
                //        MessageBox.Show("POSTANSKI MESTA JE VEC UNET!");
                //}

                int noviBroj = int.Parse(korisnikPostanskiBroj.Text);
                string noviNaziv = nazivOpstine.Text;
                int noviBrStan = int.Parse(brStan.Text);

                bool vecPostoji = opstine.Any(op => op.broj == noviBroj || op.naziv == noviNaziv);

                if (vecPostoji)
                {
                    // Ako postoji, prikaži upozorenje i zaustavi funkciju
                    MessageBox.Show("Opština sa tim poštanskim brojem ili nazivom već postoji!", "Greška pri unosu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; // Prekida dalje izvršavanje funkcije
                }

                opstina o = new opstina();

            o.broj = int.Parse(korisnikPostanskiBroj.Text);
            o.naziv = nazivOpstine.Text;
            o.brstan = int.Parse(brStan.Text);
            opstine.Add(o);
            listaOpstina.ItemsSource = null;
            listaOpstina.ItemsSource = opstine;

            korisnikPostanskiBroj.Text = string.Empty;
            nazivOpstine.Text = string.Empty;
            brStan.Text = string.Empty;
            
            Prosek();
                }
        }

        private void Prosek()
        {
            double ukupnoStan = 0;
        int brojac = 0;
            foreach(opstina item in opstine)
            {
                ukupnoStan += item.brstan;
                brojac++;
            }
            double prosek = brojac > 0 && ukupnoStan > 0  ? ukupnoStan / brojac : 0;

            prikazProseka.Text = prosek.ToString();
        }

        private void Button_Click_obrisi(object sender, RoutedEventArgs e)
        {
            var selected = listaOpstina.SelectedItem;
            int brojEl = opstine.Count;

            if(brojEl > 0 && selected != null)
            {
                cmdDa.Visibility = Visibility.Visible;
                cmdNe.Visibility = Visibility.Visible;
            } else
            {
                MessageBox.Show("Niste izabrali opštinu");
            }
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) // proverava da li je dugme koje je pritisnuto escape
            {
                cmdIzlaz_Click(sender, e); // ako jeste pokrece dogadjaj cmdIzlaz koja ce ugasiti program
            }

            if (e.Key == Key.E)
            {
                // Sada proveravamo da li taster Shift NIJE pritisnut
                // Keyboard.IsKeyDown(Key.LeftShift) proverava levi shift
                // Keyboard.IsKeyDown(Key.RightShift) proverava desni shift

                bool isShiftPressed = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);

                // Proveravamo stanje Caps Lock tastera (Console.CapsLock je true ako je uključen)
                bool isCapsLockOn = Console.CapsLock;

                // Ako Shift NIJE pritisnut, onda je to malo 'e'
                if (isShiftPressed || isCapsLockOn)
                {
                    //MessageBox.Show("Pritisnuto je veliko slovo E!");
                    // Ovde ide kod koji zelite da se izvrsi samo za malo 'e'
                }
            }

        }

        private void cmdIzlaz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdDa_Click(object sender, RoutedEventArgs e)
        {
            var selected = listaOpstina.SelectedItem as opstina;
            //opstina selected = listaOpstina.SelectedItem as opstina;
            if (selected != null)
            {
                opstine.Remove(selected);
                listaOpstina.ItemsSource = null;
                listaOpstina.ItemsSource = opstine;
                Prosek();
            }
            cmdDa.Visibility = Visibility.Hidden;
            cmdNe.Visibility = Visibility.Hidden;
        }

        private void cmdNe_Click(object sender, RoutedEventArgs e)
        {

            cmdDa.Visibility = Visibility.Hidden;
            cmdNe.Visibility = Visibility.Hidden;

        }
    }
}
