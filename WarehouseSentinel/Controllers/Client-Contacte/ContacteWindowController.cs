using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSentinel.Models;

namespace WarehouseSentinel.Controllers
{
    public enum modeControllerContacte
    {
        AFEGIR, MODIFICAR
    }

    public class ContacteWindowController
    {
        private SentinelDBEntities context;
        private modeControllerContacte mode;
        private TContacte tContacte;

        public ContacteWindowController(SentinelDBEntities context)
        {
            this.context = context;
            this.mode = mode;
            tContacte = new TContacte(context);
        }

        internal string afegeixContacte(client client, contacte contacte)
        {
            try
            {
                contacte.Client_CIF = client.CIF;

                tContacte.add(contacte);
                return "El contacte del client " + client.nom + @" s'ha afegit correctament.";
            }
            catch
            {
                return "El contacte del client " + client.nom + " no s'ha pogut afegir.";
            }
        }

        internal string modificaContacte(contacte contacte)
        {
            try
            {
                tContacte.modify(contacte);
                return "El contacte del client " + contacte.client.nom + " s'ha modificat correctament.";
            }
            catch (Exception ex)
            {
                return "El contacte del client " + contacte.client.nom + " no s'ha pogut modificar.";
            }
        }
    }
}
