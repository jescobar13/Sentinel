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
using WarehouseSentinel.Controllers.Albara;

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

        /// <summary>
        /// Constructor de la vista Gestor Albara.
        /// </summary>
        public GestorAlbaraWindow()
        {
            InitializeComponent();
            controller = new GestorAlbaraWindowController(this);
        }
    }
}
