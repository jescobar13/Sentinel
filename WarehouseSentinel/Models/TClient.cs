using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WarehouseSentinel.Models
{
    public class TClient
    {
        /// <summary>
        /// Context de la base de dades.
        /// </summary>
        SentinelDBEntities context;

        /// <summary>
        /// Constructor de la classe.
        /// </summary>
        /// <param name="context">Context de la base de dades.</param>
        public TClient(SentinelDBEntities context)
        {
            this.context = context;
        }

        /// <summary>
        /// Afegeix un client a la base de dades.
        /// </summary>
        /// <param name="c"></param>
        public void add(client c)
        {
            context.client.Add(c);
            context.SaveChanges();
        }

        /// <summary>
        /// Elimina un client de la base de dades.
        /// </summary>
        /// <param name="c">Obj. Client</param>
        public void remove(client c)
        {
            context.client.Remove(c);
            context.SaveChanges();
        }

        /// <summary>
        /// Modifica un client de la base de dades.
        /// </summary>
        /// <param name="c">Obj. Client</param>
        public void modify(client c)
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Llista els clients de la base de dades.
        /// </summary>
        /// <returns></returns>
        public List<client> getAll()
        {
            return (from taulaClient in context.client
                    select taulaClient).ToList();
        }

        /// <summary>
        /// Llista els clients de la base de dades que el seu estat és actiu.
        /// </summary>
        /// <returns>Llista dels clients actius.</returns>
        internal List<client> getClientsActius()
        {
            return (from taulaClient in context.client
                    where taulaClient.actiu == true
                    select taulaClient).ToList();
        }

        /// <summary>
        /// Llista els clients de la base de dades que el seu estat és deshabilitat.
        /// </summary>
        /// <returns>Llista dels clients deshabilitat.</returns>
        internal List<client> getClientsDeshabilitats()
        {
            return (from taulaClient in context.client
                    where taulaClient.actiu == false
                    select taulaClient).ToList();
        }
    }
}
