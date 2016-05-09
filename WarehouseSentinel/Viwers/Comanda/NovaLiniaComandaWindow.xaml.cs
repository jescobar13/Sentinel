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
using WarehouseSentinel.Controllers;
using WarehouseSentinel.Models;

namespace WarehouseSentinel.Viwers.Comanda
{
    /// <summary>
    /// Lógica de interacción para NovaLiniaComandaWindow.xaml
    /// </summary>
    public partial class NovaLiniaComandaWindow : Window
    {
        private comanda comanda;
        private ComandesWindowController controller;
        private producte producteSelected;

        public NovaLiniaComandaWindow(ComandesWindowController controller, comanda comanda)
        {
            InitializeComponent();
            this.comanda = comanda;
            this.controller = controller;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void dataGrid_productes_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid_productes.ItemsSource = controller.donemProductes();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid_productes.ItemsSource = null;
            dataGrid_productes.ItemsSource = controller.donemProductesByPattern(textBox_filtreNom.Text);
        }

        private void dataGrid_productes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_productes.SelectedItems.Count != 1) return;

            producteSelected = null;
            producteSelected = dataGrid_productes.SelectedItem as producte;

            textbox_preuKg.Text = producteSelected.preuKg.ToString();
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            liniacomanda novaLinia = new liniacomanda();
            novaLinia.preuKg = Convert.ToDouble(textbox_preuKg.Text);
            novaLinia.quantitat = Convert.ToInt32(textBox_quantitat.Text);
            novaLinia.Comanda_codi = comanda.codi;
            novaLinia.Comanda_Client_CIF = comanda.Client_CIF;
            novaLinia.Comanda_Client_Contacte_id = comanda.Client_Contacte_id;
            novaLinia.Producte_id = producteSelected.id;

            string retorna = controller.guardaLiniaComanda(novaLinia);

            MessageBox.Show(retorna, "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}
