using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSentinel.Models;

namespace WarehouseSentinel.Controllers
{
    public class ComandesWindowController
    {
        private SentinelDBEntities context;

        private TComanda tComanda;

        public ComandesWindowController(SentinelDBEntities context)
        {
            this.context = context;
            tComanda = new TComanda(context);
        }

        internal IEnumerable donemClients()
        {
            TClient tClient = new TClient(context);
            return tClient.getAll();
        }

        internal string guardaComanda(comanda comanda)
        {
            try
            {
                tComanda.add(comanda);
                return "La comanda del client " + comanda.client.nom + " s'ha guardat correctament.";
            }
            catch (Exception ex)
            {
                return "La comanda del client " + comanda.client.nom + "no s'ha pogut guardar.";
            }
        }
    }
}