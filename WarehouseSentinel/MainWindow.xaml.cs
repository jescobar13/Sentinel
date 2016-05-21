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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WarehouseSentinel.Controllers;
using WarehouseSentinel.Viwers;
using WarehouseSentinel.Viwers.Albara;
using WarehouseSentinel.Viwers.Comanda;
using WarehouseSentinel.Viwers.Producta;
using WarehouseSentinel.Viwers.Reports;

namespace WarehouseSentinel
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Controller de la vista Main
        /// </summary>
        MainWindowController controller;

        public MainWindow()
        {
            controller = new MainWindowController(this);

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //GestorComandesWindow gestorComandes = new GestorComandesWindow();
            //gestorComandes.Show();


        }

        /// <summary>
        /// Valida s'hi l'usuari existeix i si es que si obre la vista corresponent.
        /// Fa un petit control d'usuaris.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_logIn_Click(object sender, RoutedEventArgs e)
        {
            if (textBox_user.Text.Equals("jescobar"))
            {
                GestorComandesWindow gestorComandes = new GestorComandesWindow(controller);
                Hide();
            }
            else
            {
                GestorAlbaraWindow gestorComandes = new GestorAlbaraWindow(controller);
                Hide();
            }

        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you shure exit?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Hand) == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}