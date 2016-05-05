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
        ComandaWindow cw;
        private ComandesWindowController controller;

        public SelecClientWindow(ComandesWindowController controller, ComandaWindow cw)
        {
            InitializeComponent();
            this.controller = controller;
            this.cw = cw;
        }

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid_clients.ItemsSource = controller.donemClients();
        }
    }
}

