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

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            dugme.Click += ClickDugme;

            BojeComboBox.SelectionChanged += bojeComboBox_SelectionChanged;

            this.Loaded += MainWindow_Loaded;

            rbDozvoljeno.Checked += RadioButton_Checked;
            rbZabranjeno.Checked += RadioButton_Checked;

            this.MouseLeftButtonDown += Window_MouseLeftButtonDown;

        }

        private void ClickDugme(Object sender, RoutedEventArgs e)
        {
                textBox.AppendText($"{sender}");
            // dogadjaj klikom na dugme za zatvanje aplikacije
            this.Close();
        }

        private void bojeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(BojeComboBox.SelectedItem is ComboBoxItem item)
            {
                switch (item.Content.ToString())
                {
                    case "Crveno":
                        this.Background = Brushes.Red;
            textBox.Text += item.Content.ToString();
                        break;
                    case "Plavo":
                        this.Background = Brushes.Blue;
                        textBox.Text += item.Content.ToString(); 
                        break;
                    case "Zeleno":
                        this.Background = Brushes.Green;
                        textBox.Text += item.Content.ToString();
                        break;
                    default: Background = Brushes.White; 
                        break;
                }
            }
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            textBox.AppendText("Događaj: Load\n");
           
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb)
            {
                textBox.AppendText($"Događaj: RadioButton Checked - Izabrano: {rb.Content}\n");
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            textBox.Text += "Prozor se zatvara";
            if (rbZabranjeno.IsChecked == true)
            {
                e.Cancel = true;
            }

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // dogadjaj za klikom misa na prozor
            textBox.Text += "Mouse Click";
        }

    }
}
