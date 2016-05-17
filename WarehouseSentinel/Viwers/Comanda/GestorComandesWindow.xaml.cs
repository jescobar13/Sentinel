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
using WarehouseSentinel.Viwers.Producta;
using WarehouseSentinel.Viwers.Reports;

namespace WarehouseSentinel.Viwers.Comanda
{
    /// <summary>
    /// Lógica de interacción para GestorComandesWindow.xaml
    /// </summary>
    public partial class GestorComandesWindow : Window
    {
        private GestorComandesWindowController controller;


        public GestorComandesWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            controller = new GestorComandesWindowController();
            actualitzaCapcaleresComandes();
        }

        public void actualitzaCapcaleresComandes()
        {
            dataGrid_capcaleraComandes.ItemsSource = null;
            dataGrid_capcaleraComandes.ItemsSource = controller.donemComandes();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.Show();
        }

        private void menuItem_obrirProductes_Click(object sender, RoutedEventArgs e)
        {
            ProducteWindow producteWindow = new ProducteWindow();
            producteWindow.Show();
        }

        private void btn_novaComanda_Click(object sender, RoutedEventArgs e)
        {
            ComandaWindow comandaWindow = new ComandaWindow(controller.getBaseContext(), new Models.comanda());
            comandaWindow.ShowDialog();
            actualitzaCapcaleresComandes();
        }

        private void btn_report_Click(object sender, RoutedEventArgs e)
        {
            ReportView rw = new ReportView();
            rw.Show();
        }
    }
}
