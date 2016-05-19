using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSentinel.Models;
using WarehouseSentinel.Viwers.Albara;

namespace WarehouseSentinel.Controllers.Albara
{
    public class GestorAlbaraWindowController
    {
        /// <summary>
        /// Context de la base de dades.
        /// </summary>
        SentinelDBEntities context;
        /// <summary>
        /// Referencia a la vista Gestor Albara.
        /// </summary>
        GestorAlbaraWindow gestorAlbaraWindow;

        /// <summary>
        /// Referencia a la taula Comanda.
        /// </summary>
        TComanda tComanda;

        /// <summary>
        /// Constructor. Controller de la vista Gestor Albara
        /// </summary>
        /// <param name="gestorAlbaraWindow">vista gestor albara.</param>
        public GestorAlbaraWindowController(GestorAlbaraWindow gestorAlbaraWindow)
        {
            context = new SentinelDBEntities();
            this.gestorAlbaraWindow = gestorAlbaraWindow;
            this.gestorAlbaraWindow.Show();

            tComanda = new TComanda(context);
        }

    }
}
