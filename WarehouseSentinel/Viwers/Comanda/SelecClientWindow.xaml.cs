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
    /// Lógica de interacción para SelecClientWindow.xaml
    /// </summary>
    public partial class SelecClientWindow : Window
    {
        /// <summary>
        /// Referencia a la vista Comanda Window
        /// </summary>
        ComandaWindow cw;
        /// <summary>
        /// Controlador de la visa Comandes.
        /// </summary>
        private ComandesWindowController controller;

        /// <summary>
        /// Constructor de la vista Seleccio d'un Client.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="cw"></param>
        public SelecClientWindow(ComandesWindowController controller, ComandaWindow cw)
        {
            InitializeComponent();
            this.controller = controller;
            this.cw = cw;
        }

        /// <summary>
        /// Selecciona el client.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_seleccionaClient_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid_clients.SelectedItems.Count == 1)
            {
                cw.client = dataGrid_clients.SelectedItem as client;
                Close();
            }
            else
                MessageBox.Show("Selecciona 1 client.", "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Carrega el datagrid amb els clients disponibles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid_clients.ItemsSource = controller.donemClients();
        }
    }
}

