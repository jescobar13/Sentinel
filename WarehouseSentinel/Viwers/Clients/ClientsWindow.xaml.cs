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
        ClientsWindowController controller;

        client clientSeleccionat;

        public ClientsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            controller = new ClientsWindowController();

            actualitzaClients(ModeVisualitzacioClients.ACTIUS);
        }

        private void actualitzaClients(ModeVisualitzacioClients mode)
        {
            dataGrid_clients.ItemsSource = null;
            dataGrid_clients.ItemsSource = controller.donemClients(mode);
        }

        private void dataGrid_clients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clientSeleccionat = dataGrid_clients.SelectedItem as client;
        }

        private void listView_contactes_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as ListView).ItemsSource = null;
            (sender as ListView).ItemsSource = controller.donemContactes(clientSeleccionat);
            clientSeleccionat = null;
        }

        private void menuItem_tencarClients_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_AfegirClient_Click(object sender, RoutedEventArgs e)
        {
            AfegirClientWindow afegirClientWindow = new AfegirClientWindow(
                new client(), modeControllerClient.afegir, controller.getBaseContext());
            afegirClientWindow.ShowDialog();
            actualitzaClients(ModeVisualitzacioClients.ACTIUS);
        }

        private void btn_ModificarClient_Click(object sender, RoutedEventArgs e)
        {
            AfegirClientWindow modificarClientWindow = new AfegirClientWindow(
                dataGrid_clients.SelectedItem as client, modeControllerClient.modificar, controller.getBaseContext());
            modificarClientWindow.ShowDialog();
            actualitzaClients(ModeVisualitzacioClients.ACTIUS);
        }

        

        private void textBox_filtre_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltratPer filtra = FiltratPer.CIF;

            if (radioButton_filtreNom.IsChecked == true)
                filtra = FiltratPer.NOM;

            if (radioButton_filtreCIF.IsChecked == true)
                filtra = FiltratPer.CIF;

            if (radioButton_filtreCodiPostal.IsChecked == true)
                filtra = FiltratPer.CODIPOSTAL;

            if (radioButton_filtreCognom.IsChecked == true)
                filtra = FiltratPer.COGNOM;

            if (radioButton_filtrePais.IsChecked == true)
                filtra = FiltratPer.PAIS;

            dataGrid_clients.ItemsSource = null;
            dataGrid_clients.ItemsSource = controller.donemClientsByPattern(textBox_filtre.Text, filtra);
        }

        private void btn_NetejaFiltres_Click(object sender, RoutedEventArgs e)
        {
            textBox_filtre.Text = "";
            radioButton_filtreCIF.IsChecked = true;

            radioButton_visualitzaActius.IsChecked = true;
        }

        private void radioButton_visualitzaActius_Checked(object sender, RoutedEventArgs e)
        {
            actualitzaClients(ModeVisualitzacioClients.ACTIUS);
        }

        private void radioButton_visualitzaDeshabilitats_Checked(object sender, RoutedEventArgs e)
        {
            actualitzaClients(ModeVisualitzacioClients.DESHABILITATS);
        }

        private void radioButton_visualitzaTots_Checked(object sender, RoutedEventArgs e)
        {
            actualitzaClients(ModeVisualitzacioClients.TOTS);
        }

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
