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
    /// Lógica de interacción para ContacteWindow.xaml
    /// </summary>
    public partial class ContacteWindow : Window
    {
        /// <summary>
        /// Referencia obj. client.
        /// </summary>
        private client client;
        /// <summary>
        /// Referencia obj. contacte.
        /// </summary>
        private contacte contacte;
        /// <summary>
        /// Controller de la vista Contacte.
        /// </summary>
        private ContacteWindowController controller;
        /// <summary>
        /// Mode del controller.
        /// </summary>
        private modeControllerContacte mode;

        /// <summary>
        /// Constructor de la vista Contacte Window.
        /// </summary>
        /// <param name="context">Context de la base de dades.</param>
        /// <param name="c">obj. client</param>
        /// <param name="con">obj. contacte</param>
        /// <param name="mode"></param>
        public ContacteWindow(SentinelDBEntities context, client c, contacte con, modeControllerContacte mode)
        {
            InitializeComponent();
            client = c;
            contacte = con;
            this.mode = mode;
            controller = new ContacteWindowController(context);
        }

        /// <summary>
        /// Carrega el contingut dels objectes client i contactes a la vista segons el mode 
        /// en què s'ha obert la vista.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            label_nomEmpresa.Content = client.nom;

            textBox_nomContacte.Text = contacte.personaNom;
            textBox_telef1.Text = contacte.telef;
            textBox_telef2.Text = contacte.telef2;
            textBox_mobil1.Text = contacte.mob;
            textBox_mobil2.Text = contacte.mob2;
            textBox_correuElectronic.Text = contacte.correuElectronic;
            
            switch (mode)
            {
                case modeControllerContacte.AFEGIR:
                    btn_AplicarCanvisContacte.Visibility = Visibility.Hidden;
                    break;

                case modeControllerContacte.MODIFICAR:
                    btn_AfegirContacte.Visibility = Visibility.Hidden;
                    break;
            }
        }

        /// <summary>
        /// Afegeix el client a la base de dades.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AfegirClient_Click(object sender, RoutedEventArgs e)
        {
            contacte.personaNom = textBox_nomContacte.Text;
            contacte.telef = textBox_telef1.Text;
            contacte.telef2 = textBox_telef2.Text;
            contacte.mob = textBox_mobil1.Text;
            contacte.mob2 = textBox_mobil2.Text;
            contacte.correuElectronic = textBox_correuElectronic.Text;

            string retorna = controller.afegeixContacte(client, contacte);

            MessageBox.Show(retorna, "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Aplica les modificacions al contacte carregat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AplicarCanvisClient_Click(object sender, RoutedEventArgs e)
        {
            contacte.personaNom = textBox_nomContacte.Text;
            contacte.telef = textBox_telef1.Text;
            contacte.telef2 = textBox_telef2.Text;
            contacte.mob = textBox_mobil1.Text;
            contacte.mob2 = textBox_mobil2.Text;
            contacte.correuElectronic = textBox_correuElectronic.Text;

            string retorna = controller.modificaContacte(contacte);

            MessageBox.Show(retorna, "Informació", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}
