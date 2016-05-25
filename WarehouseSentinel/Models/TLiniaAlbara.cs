using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSentinel.Models
{
    public class TLiniaAlbara
    {
        SentinelDBEntities context;

        public TLiniaAlbara(SentinelDBEntities context)
        {
            this.context = context;
        }

        public void add(liniaalbara la) {
            context.liniaalbara.Add(la);
            context.SaveChanges();
        }

        public void modify(liniaalbara la)
        {
            context.SaveChanges();
        }

        public void remove(liniaalbara la)
        {
            context.liniaalbara.Remove(la);
            context.SaveChanges();
        }

        internal int? getNumCaixa()
        {
            return (from tLiniaAlbara in context.liniaalbara
                    select tLiniaAlbara)
                    .OrderByDescending(i => i.caixa)
                    .FirstOrDefault()
                    .caixa;
        }
    }
}
