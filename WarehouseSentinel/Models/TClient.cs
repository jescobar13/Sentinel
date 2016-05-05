using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WarehouseSentinel.Models
{
    public class TClient
    {
        SentinelDBEntities context;

        public TClient(SentinelDBEntities context)
        {
            this.context = context;
        }

        public void add(client c)
        {
            context.client.Add(c);
            context.SaveChanges();
        }

        public void remove(client c)
        {
            context.client.Remove(c);
            context.SaveChanges();
        }

        public void modify(client c)
        {
            context.SaveChanges();
        }

        public List<client> getAll()
        {
            return (from taulaClient in context.client
                    select taulaClient).ToList();
        }

        internal List<client> getClientsActius()
        {
            return (from taulaClient in context.client
                    where taulaClient.actiu == true
                    select taulaClient).ToList();
        }

        internal List<client> getClientsDeshabilitats()
        {
            return (from taulaClient in context.client
                    where taulaClient.actiu == false
                    select taulaClient).ToList();
        }
    }
}
