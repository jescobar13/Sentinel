﻿using System;
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

        private void actualitzaLiniesComanda(comanda c)
        {
            listView_liniesComanda.ItemsSource = null;
            listView_liniesComanda.ItemsSource = controller.donemLiniesComanda(c.codi);
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

        private void btn_acceptarCapcaleraComanda_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker_dataComanda.Text.Equals("") || datePicker_dataEntrega.Text.Equals(""))
            {
                MessageBox.Show("Selecciona les dates corresponents.", "Informacio",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            comanda.dataComanda = Convert.ToDateTime(string.Format("{0:MM/dd/yyyy}", datePicker_dataComanda));
            comanda.dataEntrega = Convert.ToDateTime(string.Format("{0:MM/dd/yyyy}", datePicker_dataEntrega));
            comanda.Client_CIF = client.CIF;

            string retorna = controller.guardaComanda(comanda);

            listView_liniesComanda.IsEnabled = true;
            btn_novaLiniaComanda.IsEnabled = true;
            btn_eliminarLiniaComanda.IsEnabled = true;
            btn_acceptarComanda.IsEnabled = true;

            actualitzaLiniesComanda(comanda);

            MessageBox.Show(retorna, "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btn_novaLiniaComanda_Click(object sender, RoutedEventArgs e)
        {
            NovaLiniaComandaWindow novaLinia = new NovaLiniaComandaWindow(controller, comanda);
            novaLinia.ShowDialog();
            actualitzaLiniesComanda(comanda);
        }

        private void btn_eliminarLiniaComanda_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_acceptarComanda_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}

