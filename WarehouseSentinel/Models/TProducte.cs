using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSentinel.Models
{
    public class TProducte
    {
        /// <summary>
        /// Contexte de la base de dades.
        /// </summary>
        private SentinelDBEntities context;

        /// <summary>
        /// Constructor de la classe TProducte
        /// </summary>
        /// <param name="context">Context de la bd.</param>
        public TProducte(SentinelDBEntities context)
        {
            this.context = context;
        }

        /// <summary>
        /// Afegeix un producte a la bd.
        /// </summary>
        /// <param name="p">obj. producte</param>
        public void add(producte p)
        {
            context.producte.Add(p);
            context.SaveChanges();
        }

        /// <summary>
        /// Modifica un producte a la bd.
        /// </summary>
        /// <param name="p">obj. producte</param>
        public void modify(producte p)
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Elimina un producte a la bd.
        /// </summary>
        /// <param name="p">obj. producte</param>
        public void remove(producte p)
        {
            context.producte.Remove(p);
            context.SaveChanges();
        }

        /// <summary>
        /// Retorna tots els productes a de la bd.
        /// </summary>
        /// <returns>Llista de productes.</returns>
        internal List<producte> getAll()
        {
            return (from taulaProducte in context.producte
                    select taulaProducte).ToList();
        }

        internal producte getByID(int producte_id)
        {
            return (from tProducte in context.producte
                    where tProducte.id == producte_id
                    select tProducte)
                    .FirstOrDefault();
        }
    }
}
