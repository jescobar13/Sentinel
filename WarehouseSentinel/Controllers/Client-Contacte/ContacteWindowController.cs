using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSentinel.Models;

namespace WarehouseSentinel.Controllers
{
    public enum modeControllerContacte
    {
        AFEGIR, MODIFICAR
    }

    public class ContacteWindowController
    {
        /// <summary>
        /// Contexte de la base de dades.
        /// </summary>
        private SentinelDBEntities context;
        /// <summary>
        /// 
        /// </summary>
        private modeControllerContacte mode;
        /// <summary>
        /// Referencia a la taula TContacte.
        /// </summary>
        private TContacte tContacte;

        /// <summary>
        /// Constructor. Controller de la vista Contacte Window.
        /// </summary>
        /// <param name="context">Context de la base de dades.</param>
        public ContacteWindowController(SentinelDBEntities context)
        {
            this.context = context;
            this.mode = mode;
            tContacte = new TContacte(context);
        }

        /// <summary>
        /// Afegeix un contacte a la base de dades.
        /// </summary>
        /// <param name="client">obj. client</param>
        /// <param name="contacte">obj. contacte</param>
        /// <returns>Missatge de retorn</returns>
        internal string afegeixContacte(client client, contacte contacte)
        {
            try
            {
                contacte.Client_CIF = client.CIF;

                tContacte.add(contacte);
                return "El contacte del client " + client.nom + @" s'ha afegit correctament.";
            }
            catch
            {
                return "El contacte del client " + client.nom + " no s'ha pogut afegir.";
            }
        }

        /// <summary>
        /// Modifica un contacte a la base de dades.
        /// </summary>
        /// <param name="contacte">obj. contacte</param>
        /// <returns>Missatge de retorn.</returns>
        internal string modificaContacte(contacte contacte)
        {
            try
            {
                tContacte.modify(contacte);
                return "El contacte del client " + contacte.client.nom + " s'ha modificat correctament.";
            }
            catch (Exception ex)
            {
                return "El contacte del client " + contacte.client.nom + " no s'ha pogut modificar.";
            }
        }
    }
}
