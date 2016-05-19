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
        /// <summary>
        /// Controlador del gestor de comandes
        /// </summary>
        private GestorComandesWindowController controller;


        public GestorComandesWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Carrega les comandes de la base de dades.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            controller = new GestorComandesWindowController();
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
