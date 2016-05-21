using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSentinel.Models;
using WarehouseSentinel.Viwers.Comanda;

namespace WarehouseSentinel.Controllers
{
    public class GestorComandesWindowController
    {
        /// <summary>
        /// Context de la base de dades.
        /// </summary>
        private SentinelDBEntities context;
        /// <summary>
        /// Referencia al obj TComanda que permet accedir a la taula Comandes
        /// de la base de dades.
        /// </summary>
        private TComanda tComanda;
        /// <summary>
        /// Referencia al obj TLiniaComanda que permet accedir a la taula 
        /// LiniesComanda de la base de dades.
        /// </summary>
        private TLiniaComanda tLiniaComanda;

        private GestorComandesWindow gestorComandesWindow;

        public GestorComandesWindowController()
        {

        }

        /// <summary>
        /// Constructor del controller Gestor de Comandes. Constructor.
        /// </summary>
        public GestorComandesWindowController(GestorComandesWindow gestorComandesWindow)
        {
            context = new SentinelDBEntities();
            tComanda = new TComanda(context);
            tLiniaComanda = new TLiniaComanda(context);
            this.gestorComandesWindow = gestorComandesWindow;
            this.gestorComandesWindow.Show();
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
        /// Retorna les comandes de la base de dades.
        /// </summary>
        /// <returns>Llista de comandes.</returns>
        internal IEnumerable donemComandes()
        {
            return tComanda.getAll();
        }

        /// <summary>
        /// Retorna les comandes de la base de dades segons el CIF d'un client.
        /// </summary>
        /// <param name="cif">Cif d'un client</param>
        /// <returns>Llista de Comandes.</returns>
        internal IEnumerable donemComandesByCif(string cif)
        {
            cif = "S3959337A";

            return tComanda.getByCIFClient(cif);
        }
    }
}
