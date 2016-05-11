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
        private TLiniaComanda tLiniaComanda;
        private List<producte> memoriaProducte;

        public ComandesWindowController(SentinelDBEntities context)
        {
            this.context = context;
            tComanda = new TComanda(context);
            tLiniaComanda = new TLiniaComanda(context);
        }

        internal IEnumerable donemClients()
        {
            TClient tClient = new TClient(context);
            return tClient.getClientsActius();
        }

        internal bool guardaComanda(comanda comanda)
        {
            try
            {
                tComanda.add(comanda);
                //return "La comanda del client " + comanda.client.nom + " s'ha guardat correctament.";
                return true;
            }
            catch (Exception ex)
            {
                return false;
               // return "La comanda del client " + comanda.client.nom + "no s'ha pogut guardar.";
            }
        }

        internal string guardaLiniaComanda(liniacomanda liniaComanda)
        {
            try
            {
                tLiniaComanda.add(liniaComanda);
                return "It has been added correctly.";
            }
            catch (Exception ex)
            {
                return "It has not been able to add.";
            }
        }

        internal IEnumerable donemLiniesComanda(int comandaID)
        {
            return tLiniaComanda.getAll(comandaID);
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