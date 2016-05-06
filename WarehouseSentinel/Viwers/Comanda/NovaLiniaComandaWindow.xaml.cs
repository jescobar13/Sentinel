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
    /// Lógica de interacción para NovaLiniaComandaWindow.xaml
    /// </summary>
    public partial class NovaLiniaComandaWindow : Window
    {
        private comanda comanda;
        private ComandesWindowController controller;

        public NovaLiniaComandaWindow(ComandesWindowController controller, comanda comanda)
        {
            InitializeComponent();
            this.comanda = comanda;
            this.controller = controller;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void dataGrid_productes_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid_productes.ItemsSource = controller.donemProductes();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid_productes.ItemsSource = null;
            dataGrid_productes.ItemsSource = controller.donemProductesByPattern(textBox_filtreNom.Text);
        }

        private void dataGrid_productes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
