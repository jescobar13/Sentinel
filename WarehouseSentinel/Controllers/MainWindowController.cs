using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSentinel.Models;
using WarehouseSentinel.Viwers.Albara;
using WarehouseSentinel.Viwers.Comanda;

namespace WarehouseSentinel.Controllers
{
    public class MainWindowController
    {
        /// <summary>
        /// Referencia a la pantalla principal
        /// </summary>
        MainWindow mainWindow;

        SentinelDBEntities context;

        /// <summary>
        /// Referencia a la taula Log-In
        /// </summary>
        private TLog_In tLog_In;

        /// <summary>
        /// Referencia a la taula Registre.
        /// </summary>
        private TRegistre tRegistre;

        /// <summary>
        /// Constructor de la classe. Controlador.
        /// </summary>
        /// <param name="mainWindow"></param>
        public MainWindowController(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.context = new SentinelDBEntities();

            this.tLog_In = new TLog_In(context);
            this.tRegistre = new TRegistre(context);
        }

        private log_in usuariLogat;

        public void visualitzaMainWindow()
        {
            mainWindow.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool isRegistry(string user, string password)
        {
            try
            {
                usuariLogat = tLog_In.getUser(user);

                if (usuariLogat != null)
                {
                    if (usuariLogat.password.Equals(password))
                    {
                        registre reg = new registre();
                        reg.dataEntrada = DateTime.Now;
                        reg.log_in = usuariLogat;

                        tRegistre.add(reg);
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void obreFinestre()
        {
            if (usuariLogat.permisos.Equals("gestor"))
            {
                GestorComandesWindow gestorComandes = new GestorComandesWindow(this);
                mainWindow.Hide();
            }
            else if(usuariLogat.permisos.Equals("magatzem"))
            {
                GestorAlbaraWindow gestorComandes = new GestorAlbaraWindow(this);
                mainWindow.Hide();
            }
        }
    }
}
