using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSentinel.Controllers
{
    public class MainWindowController
    {
        /// <summary>
        /// Referencia a la pantalla principal
        /// </summary>
        MainWindow mainWindow;

        /// <summary>
        /// Constructor de la classe. Controlador.
        /// </summary>
        /// <param name="mainWindow"></param>
        public MainWindowController(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void visualitzaMainWindow()
        {
            mainWindow.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
