using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSentinel.Models;

namespace WarehouseSentinel.Controllers
{
    public enum modeControllerClient
    {
        afegir, modificar
    }

    public class AfegirClientWindowController
    {
        /// <summary>
        /// Referencia a la taula Client.
        /// </summary>
        TClient tClient;
        /// <summary>
        /// Referencia a la taula Contacte.
        /// </summary>
        TContacte tContacte;

        /// <summary>
        /// Constructor. Controller de la vista Afegir Client.
        /// </summary>
        /// <param name="context"></param>
        public AfegirClientWindowController(SentinelDBEntities context)
        {
            tClient = new TClient(context);
            tContacte = new TContacte(context);
        }

        /// <summary>
        /// Afegeix un client a la base de dades.
        /// </summary>
        /// <param name="c">obj. client</param>
        /// <returns>Missatge de retorn.</returns>
        internal string afegeix(client c)
        {
            try
            {
                tClient.add(c);
                return "El client " + c.nom.ToUpper() + " s'ha afegit correctament.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "El client " + c.nom.ToUpper() + " no s'ha pogut afegir.";
            }
        }

        /// <summary>
        /// Modifica un client de la base de dades.
        /// </summary>
        /// <param name="c">obj. client</param>
        /// <returns>Missatge de retorn</returns>
        internal string modifica(client c)
        {
            try
            {
                tClient.modify(c);
                return "El client " + c.nom.ToUpper() + " s'ha modificat correctament.";
            }
            catch (Exception ex)
            {
                return "El client " + c.nom.ToUpper() + " no s'ha pogut modificar.";
            }
        }

        /// <summary>
        /// Llista els contactes de la base de dades segons un client.
        /// </summary>
        /// <param name="client">obj. client</param>
        /// <returns>Llista de contactes.</returns>
        internal IEnumerable donemContactes(client client)
        {
            return tContacte.getByClient(client);
        }

        /// <summary>
        /// Elimina un contacte de la base de dades.
        /// </summary>
        /// <param name="contacte">obj. contacte</param>
        /// <returns>Missatge de retorn.</returns>
        internal string eliminaContacte(contacte contacte)
        {
            try
            {
                tContacte.remove(contacte);
                return "El contacte s'ha eliminat correctament.";
            }
            catch (Exception ex)
            {
                return "El contacte no s'ha pogut eliminar.";
            }
        }
    }
}
