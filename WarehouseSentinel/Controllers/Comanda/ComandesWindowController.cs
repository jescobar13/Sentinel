using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WarehouseSentinel.Models;

namespace WarehouseSentinel.Controllers
{
    public class ComandesWindowController
    {
        /// <summary>
        /// Context de la base de dades.
        /// </summary>
        private SentinelDBEntities context;

        /// <summary>
        /// Referencia de la taula Comanda.
        /// </summary>
        private TComanda tComanda;
        /// <summary>
        /// Referencia de la taula Linia Comandes.
        /// </summary>
        private TLiniaComanda tLiniaComanda;
        /// <summary>
        /// Llista de productes. Actua com a memòria per no fer 
        /// la consulta tantes vegades.
        /// </summary>
        private List<producte> memoriaProducte;

        /// <summary>
        /// Constructor. Controller de la vista Comandes Window.
        /// </summary>
        /// <param name="context">Context de la base de dades.</param>
        public ComandesWindowController(SentinelDBEntities context)
        {
            this.context = context;
            tComanda = new TComanda(context);
            tLiniaComanda = new TLiniaComanda(context);
        }

        /// <summary>
        /// Llista els clients disponibles de la base de dades.
        /// </summary>
        /// <returns>Llista de clients</returns>
        internal IEnumerable donemClients()
        {
            TClient tClient = new TClient(context);
            return tClient.getClientsActius();
        }

        internal bool modificaComanda(comanda c)
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

        /// <summary>
        /// Guarda una comanda a la base de dades.
        /// </summary>
        /// <param name="comanda">Obj. Comanda</param>
        /// <returns>True si s'ha guardat correctament; False si no s'ha pogut guardar.</returns>
        internal bool guardaComanda(comanda comanda)
        {
            try
            {
                tComanda.add(comanda);
                //return "La comanda del client " + comanda.client.nom + " s'ha guardat correctament.";
                return true;
            }
            catch (Exception ex)
            {
                return false;
               // return "La comanda del client " + comanda.client.nom + "no s'ha pogut guardar.";
            }
        }

        /// <summary>
        /// Guarda una Linia de Comanda
        /// </summary>
        /// <param name="liniaComanda">Obj. Linia de Comanda.</param>
        /// <returns>Missatge de retorn.</returns>
        internal string guardaLiniaComanda(liniacomanda liniaComanda)
        {
            try
            {
                tLiniaComanda.add(liniaComanda);
                return "It has been added correctly.";
            }
            catch (Exception ex)
            {
                return "It has not been able to add.";
            }
        }

        /// <summary>
        /// Llista la linies de comanda segons l'id d'una comanda.
        /// </summary>
        /// <param name="comandaID">Id de la comanda.</param>
        /// <returns>Llista de Linies de Comandes</returns>
        internal IEnumerable donemLiniesComanda(int comandaID)
        {
            return tLiniaComanda.getAll(comandaID);
        }

        /// <summary>
        /// Llista els productes de la base de dades.
        /// </summary>
        /// <returns>Llista de productes.</returns>
        internal IEnumerable donemProductes()
        {
            if (memoriaProducte == null)
            {
                TProducte tProducte = new TProducte(context);
                memoriaProducte = tProducte.getAll();
            }
            return memoriaProducte;
        }

        /// <summary>
        /// Llista els productes de la base de dades segons un patro.
        /// </summary>
        /// <param name="text">patro</param>
        /// <returns>Lista amb les coincidencies.</returns>
        internal IEnumerable donemProductesByPattern(string text)
        {
            Regex searchTerm = new Regex("(" + text + ")|" + text + "([a-z]|[A-Z])|([A-Z]|[a-z])");

            return (from v in memoriaProducte
                    where searchTerm.Matches(v.nom).Count > 0
                    select v);
        }

        internal bool eliminaLinaComanda(liniacomanda liniacomanda)
        {
            try
            {
                tLiniaComanda.remove(liniacomanda);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}