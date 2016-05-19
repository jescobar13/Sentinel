using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSentinel.Models;

namespace WarehouseSentinel.Controllers.Producte
{
    public class AfegirProducteWindowController
    {
        /// <summary>
        /// Context de la base de dades.
        /// </summary>
        private SentinelDBEntities context;
        /// <summary>
        /// Referencia de la TProducte, per accedir a la taula productes de la base de dades.
        /// </summary>
        private TProducte tProducte;

        /// <summary>
        /// Constructor de la classe. Controlador de la vista Afegir Producte.
        /// </summary>
        /// <param name="context">Context de la base de dades.</param>
        public AfegirProducteWindowController(SentinelDBEntities context)
        {
            this.context = context;
            tProducte = new TProducte(context);
        }

        /// <summary>
        /// Permet afegir un producte a la base de dades.
        /// </summary>
        /// <param name="producte">Obj. producte</param>
        /// <returns>Missatge d'error.</returns>
        internal string afegeix(producte producte)
        {
            try
            {
                tProducte.add(producte);
                return "The product " + producte.nom + " has been save correctly.";
            }
            catch (Exception ex)
            {
                return "The product " + producte.nom + " has not been save correctly.";
            }
        }

        /// <summary>
        /// Permet modificar un producte a la base de dades
        /// </summary>
        /// <param name="producte">Obj. Producte</param>
        /// <returns>Missatge de retorn.</returns>
        internal string modifica(producte producte)
        {
            try
            {
                tProducte.modify(producte);
                return "The product " + producte.nom + " was successfully updated.";
            }
            catch (Exception ex)
            {
                return "The product " + producte.nom + " could not be successfully update.";
            }
        }
    }
}
