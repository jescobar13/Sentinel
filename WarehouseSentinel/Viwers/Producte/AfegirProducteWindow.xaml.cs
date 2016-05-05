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
using System.Windows.Shapes;
using WarehouseSentinel.Controllers.Producte;
using WarehouseSentinel.Models;

namespace WarehouseSentinel.Viwers.Producte
{
    /// <summary>
    /// Lógica de interacción para AfegirProducteWindow.xaml
    /// </summary>
    public partial class AfegirProducteWindow : Window
    {
        private producte producte;
        private AfegirProducteWindowController controller;
        private modeControllerProducte mode;

        public AfegirProducteWindow(SentinelDBEntities context, producte producte, modeControllerProducte mode)
        {
            InitializeComponent();
            this.controller = new AfegirProducteWindowController(context);
            this.producte = producte;
            this.mode = mode;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBox_nomProducte.Text = producte.nom;
            textBox_preuKg.Text = producte.preuKg.ToString();
            textBox_unitatsCaixa.Text = producte.unitatCaixa.ToString();
            textBox_EAN13.Text = producte.EAN13;
            textBox_EAN13.IsEnabled = false;

            switch (mode)
            {
                case modeControllerProducte.AFEGIR:
                    btn_afegirProducte.Visibility = Visibility.Visible;
                    btn_modificarProducte.Visibility = Visibility.Hidden;
                    break;

                case modeControllerProducte.MODIFICAR:
                    btn_modificarProducte.Visibility = Visibility.Visible;
                    btn_afegirProducte.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void btn_afegirProducte_Click(object sender, RoutedEventArgs e)
        {
            producte.nom = textBox_nomProducte.Text;
            producte.preuKg = Convert.ToDouble(textBox_preuKg.Text);
            producte.unitatCaixa = Convert.ToInt32(textBox_unitatsCaixa.Text);

            string retorna = controller.afegeix(producte);

            MessageBox.Show(retorna, "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btn_modificarProducte_Click(object sender, RoutedEventArgs e)
        {
            producte.nom = textBox_nomProducte.Text;
            producte.preuKg = Convert.ToDouble(textBox_preuKg.Text);
            producte.unitatCaixa = Convert.ToInt32(textBox_unitatsCaixa.Text);

            string retorna = controller.modifica(producte);

            Close();

            MessageBox.Show(retorna, "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
