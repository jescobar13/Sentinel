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
        private SentinelDBEntities context;

        public TComanda(SentinelDBEntities context)
        {
            this.context = context;
        }

        public void add(comanda c)
        {
            context.comanda.Add(c);
            context.SaveChanges();
        }

        public void modify(comanda c)
        {
            context.SaveChanges();
        }

        public void remove(comanda c)
        {
            context.comanda.Remove(c);
            context.SaveChanges();
        }

        internal IEnumerable getAll()
        {
            return (from taulaComandes in context.comanda
                    select taulaComandes).ToList();
        }
    }
}

