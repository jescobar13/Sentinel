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
using WarehouseSentinel.Viwers.Clients;

namespace WarehouseSentinel.Viwers
{
    /// <summary>
    /// Lógica de interacción para ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window
    {
        /// <summary>
        /// Controlador de la vista Client Window
        /// </summary>
        ClientsWindowController controller;

        /// <summary>
        /// Objecte client (seleccionat).
        /// </summary>
        client clientSeleccionat;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ClientsWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event que s'executa quant la vista es carrega.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            controller = new ClientsWindowController();

            actualitzaClients(ModeVisualitzacioClients.ACTIUS);
        }

        /// <summary>
        /// Actualitza el DataGrid de Clients.
        /// </summary>
        /// <param name="mode"></param>
        private void actualitzaClients(ModeVisualitzacioClients mode)
        {
            dataGrid_clients.ItemsSource = null;
            dataGrid_clients.ItemsSource = controller.donemClients(mode);
        }

        /// <summary>
        /// Event que s'executa cada vegada que es selecciona un client diferent en el DataGrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_clients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clientSeleccionat = dataGrid_clients.SelectedItem as client;
        }

        /// <summary>
        /// Event que s'executa al carrear la listView de contactes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_contactes_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as ListView).ItemsSource = null;
            (sender as ListView).ItemsSource = controller.donemContactes(clientSeleccionat);
            clientSeleccionat = null;
        }

        /// <summary>
        /// Event associat al button de tencar Clients. Tenca la finestra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem_tencarClients_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Event que s'executa al fer clic al button afegir client. Guarda el Client a la base de dades.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AfegirClient_Click(object sender, RoutedEventArgs e)
        {
            AfegirClientWindow afegirClientWindow = new AfegirClientWindow(
                new client(), modeControllerClient.afegir, controller.getBaseContext());
            afegirClientWindow.ShowDialog();
            actualitzaClients(ModeVisualitzacioClients.ACTIUS);
        }

        /// <summary>
        /// Event que s'executa al fer clic al button modificar client. 
        /// Modifica el client seleccionat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ModificarClient_Click(object sender, RoutedEventArgs e)
        {
            AfegirClientWindow modificarClientWindow = new AfegirClientWindow(
                dataGrid_clients.SelectedItem as client, modeControllerClient.modificar, controller.getBaseContext());
            modificarClientWindow.ShowDialog();
            actualitzaClients(ModeVisualitzacioClients.ACTIUS);
        }

        
        /// <summary>
        /// Event que s'executa cada vegada que el contingut del textBox_Filtre canvia.
        /// Actualitza el DataGrid de Clients amb les coincidencies del filtra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_filtre_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltratComandaPer filtra = FiltratComandaPer.CIF;

            if (radioButton_filtreNom.IsChecked == true)
                filtra = FiltratComandaPer.NOM;

            if (radioButton_filtreCIF.IsChecked == true)
                filtra = FiltratComandaPer.CIF;

            if (radioButton_filtreCodiPostal.IsChecked == true)
                filtra = FiltratComandaPer.CODIPOSTAL;

            if (radioButton_filtreCognom.IsChecked == true)
                filtra = FiltratComandaPer.COGNOM;

            if (radioButton_filtrePais.IsChecked == true)
                filtra = FiltratComandaPer.PAIS;

            dataGrid_clients.ItemsSource = null;
            dataGrid_clients.ItemsSource = controller.donemClientsByPattern(textBox_filtre.Text, filtra);
        }

        /// <summary>
        /// Fa un reset als filtres.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_NetejaFiltres_Click(object sender, RoutedEventArgs e)
        {
            textBox_filtre.Text = "";
            radioButton_filtreCIF.IsChecked = true;

            radioButton_visualitzaActius.IsChecked = true;
        }

        /// <summary>
        /// Al estar seleccionat visualitza els clients ACTIUS.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_visualitzaActius_Checked(object sender, RoutedEventArgs e)
        {
            actualitzaClients(ModeVisualitzacioClients.ACTIUS);
        }

        /// <summary>
        /// Al estar seleccionat visualitza els clients DESHABILITATS.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_visualitzaDeshabilitats_Checked(object sender, RoutedEventArgs e)
        {
            actualitzaClients(ModeVisualitzacioClients.DESHABILITATS);
        }

        /// <summary>
        /// Al estar seleccionat visualitza TOTS els clients.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_visualitzaTots_Checked(object sender, RoutedEventArgs e)
        {
            actualitzaClients(ModeVisualitzacioClients.TOTS);
        }

        /// <summary>
        /// Deshabilita el client seleccionat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DeshabilitarClient_Click(object sender, RoutedEventArgs e)
        {
            if(dataGrid_clients.SelectedItems.Count == 1)
            {
                string retorna = controller.deshabilitaClient(dataGrid_clients.SelectedItem as client);
                actualitzaClients(ModeVisualitzacioClients.ACTIUS);
                MessageBox.Show(retorna, "Informació", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Selecciona un sol client si us plau!", "Alerta", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }
    }
}
