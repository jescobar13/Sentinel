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

namespace WarehouseSentinel.Viwers.Clients
{
    /// <summary>
    /// Lógica de interacción para AfegirClientWindow.xaml
    /// </summary>
    public partial class AfegirClientWindow : Window
    {
        /// <summary>
        /// Referencia obj. client
        /// </summary>
        private client client;
        /// <summary>
        /// Controlador de la vista Afegir Client.
        /// </summary>
        private AfegirClientWindowController controller;
        /// <summary>
        /// Mode del Controlador.
        /// </summary>
        modeControllerClient mode;
        /// <summary>
        /// Context de la base de dades.
        /// </summary>
        private SentinelDBEntities context;

        /// <summary>
        /// Constructor de la vista Afegir Client
        /// </summary>
        /// <param name="c">obj. client</param>
        /// <param name="mode">mode en el que s'obre la vista.</param>
        /// <param name="context">Context de la base de dades.</param>
        public AfegirClientWindow(client c, modeControllerClient mode, SentinelDBEntities context)
        {
            InitializeComponent();
            controller = new AfegirClientWindowController(context);
            this.client = c;
            this.mode = mode;
            this.context = context;
        }

        /// <summary>
        /// Event que actua al carregar la vista.
        /// </summary>
        private void afegirClientWindow_Loaded(object sender, RoutedEventArgs e)
        {
            textBox_CIF.Text = client.CIF;
            textBox_EmpresaNom.Text = client.nom;
            textBox_Cognom.Text = client.cognom;
            textBox_Adreca.Text = client.adreca;
            textBox_CodiPostal.Text = client.codiPostal;
            textBox_Pais.Text = client.pais;

            if (client.actiu == false)
                radioButton_estatDeshabilitat.IsChecked = true;
            else
                radioButton_estatActiu.IsChecked = true;

            switch (mode)
            {
                case modeControllerClient.modificar:
                    btn_AfegirClient.Visibility = Visibility.Hidden;
                    break;

                case modeControllerClient.afegir:
                    btn_AplicarCanvisClient.Visibility = Visibility.Hidden;
                    break;
            }
        }

        /// <summary>
        /// Event que actua cada vegada que es carrega la ListView de contactes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_contactes_Loaded(object sender, RoutedEventArgs e)
        {
            actualitzaLlistaContactes();
        }

        /// <summary>
        /// Actualitza els contactes de la listView Contactes.
        /// </summary>
        private void actualitzaLlistaContactes()
        {
            listView_contactes.ItemsSource = null;
            listView_contactes.ItemsSource = controller.donemContactes(client);
        }

        /// <summary>
        /// Event que actua al fer clic al butto d'aplicar els canvis.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AplicarCanvisClient_Click(object sender, RoutedEventArgs e)
        {
            client.nom = textBox_EmpresaNom.Text;
            client.cognom = textBox_Cognom.Text;
            client.adreca = textBox_Adreca.Text;
            client.codiPostal = textBox_CodiPostal.Text;
            client.pais = textBox_Pais.Text;

            if (radioButton_estatActiu.IsChecked == true)
                client.actiu = true;
            else
                client.actiu = false;

            string retorna = controller.modifica(client);

            MessageBox.Show(retorna, "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        /// <summary>
        /// Event que actua al clic del button Afegir Client, executa l'accio de afegir
        /// el client nou a la base de dades.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AfegirClient_Click(object sender, RoutedEventArgs e)
        {
            client.CIF = textBox_CIF.Text;
            client.nom = textBox_EmpresaNom.Text;
            client.cognom = textBox_Cognom.Text;
            client.adreca = textBox_Adreca.Text;
            client.codiPostal = textBox_CodiPostal.Text;
            client.pais = textBox_Pais.Text;

            if (radioButton_estatActiu.IsChecked == true)
                client.actiu = true;
            else
                client.actiu = false;

            string retorna = controller.afegeix(client);

            Close();
            MessageBox.Show(retorna, "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        /// <summary>
        /// Event que s'associa al button afegri Contacte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_afegirContacte_Click(object sender, RoutedEventArgs e)
        {
            ContacteWindow contacteWindow = new ContacteWindow(
                context, client, new contacte(), modeControllerContacte.AFEGIR);
            contacteWindow.ShowDialog();
            actualitzaLlistaContactes();
        }

        /// <summary>
        /// Event que s'associa al button modificar Contacte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_modificarContacte_Click(object sender, RoutedEventArgs e)
        {
            if(listView_contactes.SelectedItems.Count == 1)
            {
                ContacteWindow contacteWindow = new ContacteWindow(
                    context, client, listView_contactes.SelectedItem as contacte, 
                    modeControllerContacte.MODIFICAR);
                contacteWindow.ShowDialog();
                actualitzaLlistaContactes();
            }
            else
            {
                MessageBox.Show("Selecciona un contacte.", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Event que s'associa al button eliminar Contacte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_eliminarContacte_Click(object sender, RoutedEventArgs e)
        {
            if(listView_contactes.SelectedItems.Count == 1)
            {
                string retorna = controller.eliminaContacte(listView_contactes.SelectedItem as contacte);

                MessageBox.Show(retorna, "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if(listView_contactes.SelectedItems.Count > 1)
            {
                foreach(contacte c in listView_contactes.SelectedItems)
                {
                    controller.eliminaContacte(c);
                }
                MessageBox.Show("S'ha afectuat l'accio correctament", "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("S'ha de seleccionar minim un contacte.", "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            actualitzaLlistaContactes();
        }
    }
}
