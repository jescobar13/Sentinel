using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSentinel.Models;

namespace WarehouseSentinel.Controllers.Producte
{
    /// <summary>
    /// Enamuració del mode del Controlador de producte.
    /// </summary>
    public enum modeControllerProducte { AFEGIR, MODIFICAR }


    public class ProducteWindowController
    {
        /// <summary>
        /// Contexte de la base de dades.
        /// </summary>
        private SentinelDBEntities context;
        /// <summary>
        /// Referencia al objecte TProducte per accedir a la taula Producte.
        /// </summary>
        private TProducte tProducte;

        /// <summary>
        /// Constructor del controller de la vista producte.
        /// </summary>
        public ProducteWindowController()
        {
            context = new SentinelDBEntities();
            tProducte = new TProducte(context);
        }

        /// <summary>
        /// Serveix el context de la base de dades.
        /// </summary>
        /// <returns>Context de la base de dades.</returns>
        public SentinelDBEntities getBaseContext()
        {
            return context;
        }

        /// <summary>
        /// Consulta a la base de dades tots els productes.
        /// </summary>
        /// <returns>Llista de productes de la base de dades.</returns>
        internal IEnumerable donemProductes()
        {
            return tProducte.getAll();
        }
    }
}
