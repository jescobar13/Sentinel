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
using WarehouseSentinel.Viwers.Producte;
using WarehouseSentinel.Models;

namespace WarehouseSentinel.Viwers.Producta
{
    /// <summary>
    /// Lógica de interacción para ProducteWindow.xaml
    /// </summary>
    public partial class ProducteWindow : Window
    {
        /// <summary>
        /// Controller de la vista ProducteWindow.
        /// </summary>
        private ProducteWindowController controller;

        /// <summary>
        /// Constructor de la vista Producte.
        /// </summary>
        public ProducteWindow()
        {
            InitializeComponent();
            controller = new ProducteWindowController();
        }

        /// <summary>
        /// Carrega els productes a la vista.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            actualitzaProductes();
        }

        /// <summary>
        /// Carrega els productes en el DataGrid productes.
        /// </summary>
        private void actualitzaProductes()
        {
            dataGrid_productes.ItemsSource = null;
            dataGrid_productes.ItemsSource = controller.donemProductes();
        }

        /// <summary>
        /// Afegeix el producte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AfegirProducte_Click(object sender, RoutedEventArgs e)
        {
            AfegirProducteWindow afegirProducteWindow = new AfegirProducteWindow(
                controller.getBaseContext(), new producte(), modeControllerProducte.AFEGIR);

            afegirProducteWindow.ShowDialog();
            actualitzaProductes();
        }

        /// <summary>
        /// Tenca la vista de productes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem_tencarProductes_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void textBox_filtre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Aplica els canvis fets en el producte i els guarda a la BD.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ModificarProducte_Click(object sender, RoutedEventArgs e)
        {
            AfegirProducteWindow modificaProducteWindow = new AfegirProducteWindow(
                controller.getBaseContext(), dataGrid_productes.SelectedItem as producte, 
                modeControllerProducte.MODIFICAR);

            modificaProducteWindow.ShowDialog();
            actualitzaProductes();
        }
    }
}
