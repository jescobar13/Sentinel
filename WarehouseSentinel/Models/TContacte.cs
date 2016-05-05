using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSentinel.Models
{
    public class TContacte
    {
        private SentinelDBEntities context;

        public TContacte(SentinelDBEntities context)
        {
            this.context = context;
        }

        internal void add(contacte contacte)
        {
            context.contacte.Add(contacte);
            context.SaveChanges();
        }

        internal void modify(contacte c)
        {
            context.SaveChanges();
        }

        internal void remove(contacte c)
        {
            context.contacte.Remove(c);
            context.SaveChanges();
        }

        public List<contacte> getByClient(client c)
        {
            return (from tContactes in context.contacte
                    join tClient in context.client on tContactes.Client_CIF equals tClient.CIF
                    where tContactes.Client_CIF.Equals(c.CIF)
                    select tContactes).ToList();
        }
    }
}
