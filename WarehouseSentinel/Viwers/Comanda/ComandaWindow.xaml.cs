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
        private ComandesWindowController controller;
        private comanda comanda;

        public client client { get; set; }

        public ComandaWindow(SentinelDBEntities context, comanda comanda)
        {
            InitializeComponent();
            controller = new ComandesWindowController(context);
            this.comanda = comanda;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            datePicker_dataComanda.Text = string.Format("{0:dd/MM/yyyy}", comanda.dataComanda);
            datePicker_dataEntrega.Text = string.Format("{0:dd/MM/yyyy}", comanda.dataEntrega);

        }

        private void btn_selecClient_Click(object sender, RoutedEventArgs e)
        {
            SelecClientWindow selecClientWindow = new SelecClientWindow(controller, this);
            selecClientWindow.ShowDialog();
            if (client == null) return;
            label_CIF.Content = client.CIF;
            label_codiPostal.Content = client.codiPostal;
            label_cognom.Content = client.cognom;
            label_nomEmpresa.Content = client.nom;
            label_pais.Content = client.pais;
        }

        private void btn_acceptarComanda_Click(object sender, RoutedEventArgs e)
        {
            comanda.dataComanda = Convert.ToDateTime(string.Format("{0:MM/dd/yyyy}", datePicker_dataComanda));
            comanda.dataEntrega = Convert.ToDateTime(string.Format("{0:MM/dd/yyyy}", datePicker_dataEntrega));
            comanda.Client_CIF = client.CIF;

            string retorna = controller.guardaComanda(comanda);
            Close();
            MessageBox.Show(retorna, "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

