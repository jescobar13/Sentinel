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
    /// Lógica de interacción para ComandaWindow.xaml
    /// </summary>
    public partial class ComandaWindow : Window
    {
        /// <summary>
        /// Controlador de la vista Comandes
        /// </summary>
        private ComandesWindowController controller;
        /// <summary>
        /// Referencia obj. comanda.
        /// </summary>
        private comanda comanda;

        /// <summary>
        /// Referencia obj. Client.
        /// </summary>
        public client client { get; set; }

        public ComandaWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor de la vista de Comandes
        /// </summary>
        /// <param name="context">Contexte de la base de dades.</param>
        /// <param name="comanda">Obj. comanda</param>
        public ComandaWindow(SentinelDBEntities context, comanda comanda)
        {
            InitializeComponent();
            controller = new ComandesWindowController(context);
            this.comanda = comanda;
        }

        /// <summary>
        /// Carrega les dates a la vista.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comanda.estat != null && comanda.estat.Equals("edit"))
                {
                    btn_selecClient.IsEnabled = false;
                    listView_liniesComanda.IsEnabled = true;
                    btn_novaLiniaComanda.IsEnabled = true;
                    btn_eliminarLiniaComanda.IsEnabled = true;
                    btn_acceptarComanda.IsEnabled = true;

                    datePicker_dataComanda.Text = string.Format("{0:dd/MM/yyyy}", comanda.dataComanda);
                    datePicker_dataEntrega.Text = string.Format("{0:dd/MM/yyyy}", comanda.dataEntrega);

                    label_CIF.Content = comanda.client.CIF;
                    label_codiPostal.Content = comanda.client.codiPostal;
                    label_cognom.Content = comanda.client.cognom;
                    label_nomEmpresa.Content = comanda.client.nom;
                    label_pais.Content = comanda.client.pais;

                    actualitzaLiniesComanda(comanda);
                }
            }
            catch
            {
                MessageBox.Show("Error loading data. This window will be close.", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }

        }

        /// <summary>
        /// Actualitza les linies d'una comanda
        /// </summary>
        /// <param name="c">obj. comanda</param>
        private void actualitzaLiniesComanda(comanda c)
        {
            listView_liniesComanda.ItemsSource = null;
            listView_liniesComanda.ItemsSource = controller.donemLiniesComanda(c.codi);
        }

        /// <summary>
        /// Event que sexecuta al clicar el button seleccionar Client. 
        /// Controla el fet de seleccionar un client.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_selecClient_Click(object sender, RoutedEventArgs e)
        {
            listView_liniesComanda.IsEnabled = false;
            btn_novaLiniaComanda.IsEnabled = false;
            btn_eliminarLiniaComanda.IsEnabled = false;
            btn_acceptarComanda.IsEnabled = false;

            if (datePicker_dataComanda.Text.Equals("") || datePicker_dataEntrega.Text.Equals(""))
            {
                MessageBox.Show("Selec all dates.", "Information",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (seleccionaClient())
            {
                confirmarComandaCapcalera();
            }
        }

        /// <summary>
        /// Obre la vista de seleccio del client i carrega les seves dades.
        /// </summary>
        /// <returns>TRUE si s'ha seleccionat, FALSE si no s'ha seleccionat un client.</returns>
        private bool seleccionaClient()
        {
            SelecClientWindow selecClientWindow = new SelecClientWindow(controller, this);
            selecClientWindow.ShowDialog();

            if (client == null) return false;

            if (MessageBox.Show("Are you sure to want select this client?  " + client.nom.ToUpper() + " .", "Information",
                MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.Cancel)
            {
                return false;
            }
            else
            {
                label_CIF.Content = client.CIF;
                label_codiPostal.Content = client.codiPostal;
                label_cognom.Content = client.cognom;
                label_nomEmpresa.Content = client.nom;
                label_pais.Content = client.pais;
                return true;
            }
        }

        /// <summary>
        /// Guarda la capçalera de la comanda.
        /// </summary>
        /// <returns>TRUE s'hi s'ha pogut guardar.</returns>
        private bool confirmarComandaCapcalera()
        {
            comanda.dataComanda = Convert.ToDateTime(string.Format("{0:MM/dd/yyyy}", datePicker_dataComanda));
            comanda.dataEntrega = Convert.ToDateTime(string.Format("{0:MM/dd/yyyy}", datePicker_dataEntrega));
            comanda.Client_CIF = client.CIF;
            comanda.estat = "construccio";

            if (controller.guardaComanda(comanda))
            {
                listView_liniesComanda.IsEnabled = true;
                btn_novaLiniaComanda.IsEnabled = true;
                btn_eliminarLiniaComanda.IsEnabled = true;
                btn_acceptarComanda.IsEnabled = true;

                actualitzaLiniesComanda(comanda);

                return true;
            }
            return false;
        }

        /// <summary>
        /// Obre la vista per introduir una nova linia de comanda.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_novaLiniaComanda_Click(object sender, RoutedEventArgs e)
        {
            NovaLiniaComandaWindow novaLinia = new NovaLiniaComandaWindow(controller, comanda);
            novaLinia.ShowDialog();
            actualitzaLiniesComanda(comanda);
        }

        /// <summary>
        /// Elimina la comanda seleccionada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_eliminarLiniaComanda_Click(object sender, RoutedEventArgs e)
        {
            if(listView_liniesComanda.SelectedItems.Count == 1)
            {
                if (controller.eliminaLinaComanda(listView_liniesComanda.SelectedItem as liniacomanda))
                    MessageBox.Show("The line has been deleted successfully.", "Information", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("The line hasn't been deleted successfully.", "Information", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                foreach(liniacomanda li in listView_liniesComanda.SelectedItems)
                    if(!controller.eliminaLinaComanda(li))
                        MessageBox.Show("There was a problem deleting a line.", "Information", 
                            MessageBoxButton.OK, MessageBoxImage.Information);

                MessageBox.Show(listView_liniesComanda.SelectedItems.Count + " deleted records correctly", "Information",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            actualitzaLiniesComanda(comanda);
        }

        /// <summary>
        /// Confirma la comanda construida i surt de la vista.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_acceptarComanda_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("The order is the pending queue.", "Information",
                MessageBoxButton.OKCancel, MessageBoxImage.Information)
                == MessageBoxResult.Cancel) return;

            comanda.estat = "pendent";
            controller.modificaComanda(comanda);
            Close();
        }

        private void listView_liniesComanda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView_liniesComanda.SelectedItems.Count > 1) return;

            liniacomanda li = listView_liniesComanda.SelectedItem as liniacomanda;

            lbl_nomProducte.Content = li.producte.nom;
            lbl_preuKg.Content = string.Format("{0} €", li.preuKg.ToString());
            lbl_unitatCaixa.Content = string.Format("{0} u.", li.producte.unitatCaixa.ToString());
        }
    }


}


