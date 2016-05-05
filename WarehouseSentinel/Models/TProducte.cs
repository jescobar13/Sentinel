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
        private SentinelDBEntities context;

        public TProducte(SentinelDBEntities context)
        {
            this.context = context;
        }

        public void add(producte p)
        {
            context.producte.Add(p);
            context.SaveChanges();
        }

        public void modify(producte p)
        {
            context.SaveChanges();
        }

        public void remove(producte p)
        {
            context.producte.Remove(p);
            context.SaveChanges();
        }

        internal List<producte> getAll()
        {
            return (from taulaProducte in context.producte
                    select taulaProducte).ToList();
        }
    }
}
