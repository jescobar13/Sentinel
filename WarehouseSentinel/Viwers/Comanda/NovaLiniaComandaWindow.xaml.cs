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
        /// <summary>
        /// Referencia obj. comanda
        /// </summary>
        private comanda comanda;
        /// <summary>
        /// Controlador de la vista Comandes.
        /// </summary>
        private ComandesWindowController controller;
        /// <summary>
        /// Referencia obj. producte seleccionat.
        /// </summary>
        private producte producteSelected;

        /// <summary>
        /// Constructor de la vista Nova Linia Comanda.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="comanda"></param>
        public NovaLiniaComandaWindow(ComandesWindowController controller, comanda comanda)
        {
            InitializeComponent();
            this.comanda = comanda;
            this.controller = controller;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Actualitza el dataGrid de Productes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_productes_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid_productes.ItemsSource = controller.donemProductes();
        }

        /// <summary>
        /// Executa el filtrat i actualitza el DataGrid amb les coincidencies.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid_productes.ItemsSource = null;
            dataGrid_productes.ItemsSource = controller.donemProductesByPattern(textBox_filtreNom.Text);
        }

        /// <summary>
        /// Selecciona un producte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_productes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_productes.SelectedItems.Count != 1) return;

            producteSelected = null;
            producteSelected = dataGrid_productes.SelectedItem as producte;

            textbox_preuKg.Text = producteSelected.preuKg.ToString();
        }

        /// <summary>
        /// Afegeix la nova linia de comandes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch
            {
                MessageBox.Show("You must fill in all the boxes.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }
    }
}
