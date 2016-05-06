using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WarehouseSentinel.Models;

namespace WarehouseSentinel.Controllers
{
    public class ComandesWindowController
    {
        private SentinelDBEntities context;

        private TComanda tComanda;
        private List<producte> memoriaProducte;

        public ComandesWindowController(SentinelDBEntities context)
        {
            this.context = context;
            tComanda = new TComanda(context);
        }

        internal IEnumerable donemClients()
        {
            TClient tClient = new TClient(context);
            return tClient.getClientsActius();
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

        internal IEnumerable donemProductes()
        {
            if (memoriaProducte == null)
            {
                TProducte tProducte = new TProducte(context);
                memoriaProducte = tProducte.getAll();
            }
            return memoriaProducte;
        }

        internal IEnumerable donemProductesByPattern(string text)
        {
            Regex searchTerm = new Regex("(" + text + ")|" + text + "([a-z]|[A-Z])|([A-Z]|[a-z])");

            return (from v in memoriaProducte
                    where searchTerm.Matches(v.nom).Count > 0
                    select v);
        }
    }
}