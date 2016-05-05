using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSentinel.Models
{
    public class TLiniaComanda
    {
        private SentinelDBEntities context;

        public TLiniaComanda(SentinelDBEntities context)
        {
            this.context = context;
        }

        public void add(liniacomanda lc)
        {
            context.liniacomanda.Add(lc);
            context.SaveChanges();
        }

        public void modify(liniacomanda lc)
        {
            context.SaveChanges();
        }

        public void remove(liniacomanda lc)
        {
            context.liniacomanda.Remove(lc);
            context.SaveChanges();
        }
    }
}

