using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSentinel.Models
{
    public class TLiniaComanda
    {
        /// <summary>
        /// Context de la base de dades.
        /// </summary>
        private SentinelDBEntities context;

        /// <summary>
        /// Constructor de la base de dades.
        /// </summary>
        /// <param name="context">Contexte de la base de dades.</param>
        public TLiniaComanda(SentinelDBEntities context)
        {
            this.context = context;
        }

        /// <summary>
        /// Afegeix una linia de comanda a la base de dades.
        /// </summary>
        /// <param name="lc">obj. Linia de comanda.</param>
        public void add(liniacomanda lc)
        {
            context.liniacomanda.Add(lc);
            context.SaveChanges();
        }

        /// <summary>
        /// Modifica una lina de comanda de la base de dades.
        /// </summary>
        /// <param name="lc">obj. Linia de comanda.</param>
        public void modify(liniacomanda lc)
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Elimina una linia de comanda de la base de dades.
        /// </summary>
        /// <param name="lc">obj. Linia de comanda.</param>
        public void remove(liniacomanda lc)
        {
            context.liniacomanda.Remove(lc);
            context.SaveChanges();
        }

        /// <summary>
        /// Llista tots els obj. Linies de Comanda a partir de una comanda.
        /// </summary>
        /// <param name="comandaID">ID de comanda.</param>
        /// <returns>Llista de linies de comanda.</returns>
        internal IEnumerable getAll(int comandaID)
        {
            return (from tLiniacomanda in context.liniacomanda
                    where tLiniacomanda.Comanda_codi == comandaID
                    select tLiniacomanda)
                    .ToList();
        }
    }
}

