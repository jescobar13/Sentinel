using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSentinel.Models
{
    public class TAlbara
    {
        private SentinelDBEntities context;

        public TAlbara(SentinelDBEntities context)
        {
            this.context = context;
        }

        public void add(albara a)
        {
            context.albara.Add(a);
            context.SaveChanges();
        }

        public void modify(albara a)
        {
            context.SaveChanges();
        }

        public void remove(albara a)
        {
            context.albara.Remove(a);
            context.SaveChanges();
        }
    }
}
