﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
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
using WarehouseSentinel.Controllers.Albara;
using WarehouseSentinel.Models;

namespace WarehouseSentinel.Viwers.Albara
{
    /// <summary>
    /// Lógica de interacción para GestorAlbaraWindow.xaml
    /// </summary>
    public partial class GestorAlbaraWindow : Window
    {
        /// <summary>
        /// Controlador de la vista.
        /// </summary>
        GestorAlbaraWindowController controller;
        MainWindowController mainWindowController;

        /// <summary>
        /// Comanda seleccionada;
        /// </summary>
        private CapComanda CapComandaSelected { get; set; }

        /// <summary>
        /// Determina si hi ha una capçalera de comanda seleccionada.
        /// </summary>
        private bool Is_selected_CapComanda { get; set; }

        /// <summary>
        /// Determina si hi ha una linia de comanda seleccionada.
        /// </summary>
        private bool Is_selected_liniaComanda { get; set; }

        /// <summary>
        /// Constructor de la vista Gestor Albara.
        /// </summary>
        public GestorAlbaraWindow(MainWindowController mainWindowController)
        {
            InitializeComponent();
            controller = new GestorAlbaraWindowController(this);
            this.mainWindowController = mainWindowController;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindowController.visualitzaMainWindow();
        }

        private void dataGrid_capcaleraComandes_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid_capcaleraComandes.ItemsSource = controller.donemCapcaleresComandes();
        }

        private void dataGrid_capcaleraComandes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_capcaleraComandes.SelectedItems.Count != 1) return;

            if(MessageBox.Show("Are you sure made ​​this order ?", "Information",
                MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                CapComandaSelected = dataGrid_capcaleraComandes.SelectedItem as CapComanda;
                dataGrid_capcaleraComandes.IsEnabled = false;
                actualitzaLiniesComanda(CapComandaSelected);
            }
        }

        private void actualitzaLiniesComanda(CapComanda comanda)
        {
            dataGrid_LiniesComandes.ItemsSource = null;
            dataGrid_LiniesComandes.ItemsSource = controller.donemLiniesComanda(comanda.ComandaID);

        }

        private void btn_playComanda_Click(object sender, RoutedEventArgs e)
        {
            liniacomanda liniaComandaSeleccionada = ((FrameworkElement)sender).DataContext as liniacomanda;

            controller.processaLiniaComanda(liniaComandaSeleccionada);

        }


    }
}
