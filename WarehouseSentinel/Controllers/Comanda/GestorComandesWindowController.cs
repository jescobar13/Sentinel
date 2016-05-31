using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WarehouseSentinel.Models;
using WarehouseSentinel.Viwers.Comanda;

namespace WarehouseSentinel.Controllers
{
    public enum FiltraComandaPer
    {
        CIF, NOM
    }

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

        private List<comanda> memoria;

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
            if (memoria == null)
                memoria = new List<comanda>();

            memoria.Clear();
            memoria = tComanda.getAll();
            return memoria;
        }

        internal IEnumerable donemComandes(string mode)
        {
            if (memoria == null)
                memoria = new List<comanda>();

            memoria.Clear();
            memoria = tComanda.getAll(mode);
            return memoria;
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

        internal IEnumerable donemLiniesComandaByCodiComanda(int codi)
        {
            return tLiniaComanda.getByCodiComanda(codi);
        }

        internal bool guardaComanda(comanda c)
        {
            try
            {
                tComanda.modify(c);
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal bool eliminaComanda(comanda c)
        {
            try
            {
                tComanda.remove(c);
                return true;
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// Serveix els clients que coincideixen amb el patro passat.
        /// </summary>
        /// <param name="text">patro</param>
        /// <param name="filtra">Filtrat per CIF / NOM</param>
        /// <returns>Llista de Clients</returns>
        internal IEnumerable donemClientsByPattern(string text, FiltratComandaPer filtra)
        {
            Regex searchTerm;
            IEnumerable<comanda> coinciden;

            switch (filtra)
            {
                case FiltratComandaPer.NOM:
                    searchTerm = new Regex("(" + text + ")|" + text + "([a-z]|[A-Z])");

                    coinciden = (from v in memoria
                                 where searchTerm.Matches(v.client.nom).Count > 0
                                 select v);

                    return coinciden;
                    break;

                case FiltratComandaPer.CIF:
                    searchTerm = new Regex("(" + text + ")|" + text + "([a-z]|[A-Z]|[0-9])");

                    coinciden = (from v in memoria
                                 where searchTerm.Matches(v.Client_CIF).Count > 0
                                 select v);

                    return coinciden;
                    break;
            }
            return null;
        }
    }
}
