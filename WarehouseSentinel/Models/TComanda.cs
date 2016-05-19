using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSentinel.Models
{
    public class TComanda
    {
        /// <summary>
        /// Context de la base de dades.
        /// </summary>
        private SentinelDBEntities context;

        /// <summary>
        /// Constructor de la classe
        /// </summary>
        /// <param name="context">Context de la base de dades.</param>
        public TComanda(SentinelDBEntities context)
        {
            this.context = context;
        }

        /// <summary>
        /// Afegeix una comanda a la base de dades.
        /// </summary>
        /// <param name="c">Obj. Comanda</param>
        public void add(comanda c)
        {
            context.comanda.Add(c);
            context.SaveChanges();
        }

        /// <summary>
        /// Modifica una comanda de la base de dades.
        /// </summary>
        /// <param name="c">Obj. Comanda</param>
        public void modify(comanda c)
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Elimina una comanda de la base de dades.
        /// </summary>
        /// <param name="c">Obj. Comanda</param>
        public void remove(comanda c)
        {
            context.comanda.Remove(c);
            context.SaveChanges();
        }

        /// <summary>
        /// Llista les comandes a la base de dades.
        /// </summary>
        /// <returns>Llista de comandes.</returns>
        internal IEnumerable getAll()
        {
            return (from taulaComandes in context.comanda
                    select taulaComandes).ToList();
        }

        /// <summary>
        /// Llista les comandes d'un client concret.
        /// </summary>
        /// <param name="cif">Camp clau del obj. client</param>
        /// <returns>Llista de comandes segons client.</returns>
        public IEnumerable getByCIFClient(string cif)
        {
            return (from tComanda in context.comanda
                    where tComanda.Client_CIF.Equals(cif)
                    select tComanda)
                    .ToList();
        }
    }
}

