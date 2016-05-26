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
using WarehouseSentinel.Viwers.Producta;
using WarehouseSentinel.Viwers.Reports;

namespace WarehouseSentinel.Viwers.Comanda
{
    /// <summary>
    /// Lógica de interacción para GestorComandesWindow.xaml
    /// </summary>
    public partial class GestorComandesWindow : Window
    {
        /// <summary>
        /// Controlador del gestor de comandes
        /// </summary>
        private GestorComandesWindowController controller;
        /// <summary>
        /// Controlador del MainWindow
        /// </summary>
        private MainWindowController mainWindowController;

        public GestorComandesWindow(MainWindowController mainWindowController)
        {
            InitializeComponent();
            controller = new GestorComandesWindowController(this);
            this.mainWindowController = mainWindowController;
        }

        /// <summary>
        /// Carrega les comandes de la base de dades.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            actualitzaCapcaleresComandes();
        }

        /// <summary>
        /// Actualitza les dades del DataGrid de comandes.
        /// </summary>
        public void actualitzaCapcaleresComandes()
        {
            dataGrid_capcaleraComandes.ItemsSource = null;
            dataGrid_capcaleraComandes.ItemsSource = controller.donemComandes();
        }

        private void actualitzaLiniesComandes()
        {
            if (comandaSeleccionada == null) return;
            dataGrid_liniaComanda.ItemsSource = null;
            dataGrid_liniaComanda.ItemsSource = controller.donemLiniesComandaByCodiComanda(comandaSeleccionada.codi);
        }

        /// <summary>
        /// Obre la vista de administració de clients.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.Show();
        }

        /// <summary>
        /// Obre la vista d'administració de productes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem_obrirProductes_Click(object sender, RoutedEventArgs e)
        {
            ProducteWindow producteWindow = new ProducteWindow();
            producteWindow.Show();
        }

        /// <summary>
        /// Obre la vista per entrar una nova comanda.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_novaComanda_Click(object sender, RoutedEventArgs e)
        {
            ComandaWindow comandaWindow = new ComandaWindow(controller.getBaseContext(), new comanda());
            comandaWindow.ShowDialog();
            actualitzaCapcaleresComandes();
        }

        private void btn_report_Click(object sender, RoutedEventArgs e)
        {
            ReportView rw = new ReportView();
            rw.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindowController.registreSortida();
            mainWindowController.visualitzaMainWindow();
        }

        private comanda comandaSeleccionada;

        private void dataGrid_capcaleraComandes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_capcaleraComandes.SelectedItems.Count > 1) return;

            comandaSeleccionada = dataGrid_capcaleraComandes.SelectedItem as comanda;

            actualitzaLiniesComandes();
        }



        private void btn_ModificarComanda_Click(object sender, RoutedEventArgs e)
        {
            comanda c = dataGrid_capcaleraComandes.SelectedItem as comanda;
            c.estat = "edit";
            if(!controller.guardaComanda(c))return;

            ComandaWindow comandaWindow = new ComandaWindow(controller.getBaseContext(), c);
            comandaWindow.ShowDialog();
            actualitzaCapcaleresComandes();
        }

        private void btn_EliminarComanda_Click(object sender, RoutedEventArgs e)
        {
            comanda c = dataGrid_capcaleraComandes.SelectedItem as comanda;
            if (controller.eliminaComanda(c))
                MessageBox.Show("The order has been successfully removed.", "Infromation",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("The order has not been successfully removed.", "Infromation",
                    MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
