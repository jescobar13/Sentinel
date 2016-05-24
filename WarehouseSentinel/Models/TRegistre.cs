using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSentinel.Models
{
    public class TRegistre
    {
        /// <summary>
        /// Context de la base de dades.
        /// </summary>
        private SentinelDBEntities context;

        /// <summary>
        /// Constructor de la base de dades.
        /// </summary>
        /// <param name="context">Contexte de la base de dades.</param>
        public TRegistre(SentinelDBEntities context)
        {
            this.context = context;
        }

        internal void add(registre reg)
        {
            context.registre.Add(reg);
            context.SaveChanges();
        }

        internal void modify(registre registreSessio)
        {
            context.SaveChanges();
        }
    }
}
