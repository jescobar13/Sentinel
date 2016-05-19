using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSentinel.Models
{
    public class TContacte
    {
        /// <summary>
        /// Context de la base de dades.
        /// </summary>
        private SentinelDBEntities context;

        /// <summary>
        /// Constructor de la base de dades.
        /// </summary>
        /// <param name="context">Contexte de la base de dades.</param>
        public TContacte(SentinelDBEntities context)
        {
            this.context = context;
        }

        /// <summary>
        /// Afegeix un contacte a la base de dades.
        /// </summary>
        /// <param name="contacte">obj. contacte</param>
        internal void add(contacte contacte)
        {
            context.contacte.Add(contacte);
            context.SaveChanges();
        }

        /// <summary>
        /// Modifica un contacte de la base de dades.
        /// </summary>
        /// <param name="c">obj. contacte</param>
        internal void modify(contacte c)
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Elimina un contacte de la base de dades.
        /// </summary>
        /// <param name="c">obj. contacte</param>
        internal void remove(contacte c)
        {

            context.contacte.Remove(c);
            context.SaveChanges();
        }
        
        /// <summary>
        /// Llista contactes segons un client de la base de dades.
        /// </summary>
        /// <param name="c">obj. client</param>
        /// <returns>Llista de la base de dades els contactes segons el client.</returns>
        public List<contacte> getByClient(client c)
        {
            return (from tContactes in context.contacte
                    join tClient in context.client on tContactes.Client_CIF equals tClient.CIF
                    where tContactes.Client_CIF.Equals(c.CIF)
                    select tContactes).ToList();
        }
    }
}
