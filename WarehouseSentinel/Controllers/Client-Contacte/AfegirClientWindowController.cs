using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSentinel.Models;

namespace WarehouseSentinel.Controllers
{
    public enum modeControllerClient
    {
        afegir, modificar
    }

    public class AfegirClientWindowController
    {
        TClient tClient;
        TContacte tContacte;

        public AfegirClientWindowController(SentinelDBEntities context)
        {
            tClient = new TClient(context);
            tContacte = new TContacte(context);
        }

        internal string afegeix(client c)
        {
            try
            {
                tClient.add(c);
                return "El client " + c.nom.ToUpper() + " s'ha afegit correctament.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "El client " + c.nom.ToUpper() + " no s'ha pogut afegir.";
            }
        }

        internal string modifica(client c)
        {
            try
            {
                tClient.modify(c);
                return "El client " + c.nom.ToUpper() + " s'ha modificat correctament.";
            }
            catch (Exception ex)
            {
                return "El client " + c.nom.ToUpper() + " no s'ha pogut modificar.";
            }
        }

        internal IEnumerable donemContactes(client client)
        {
            return tContacte.getByClient(client);
        }

        internal string eliminaContacte(contacte contacte)
        {
            try
            {
                tContacte.remove(contacte);
                return "El contacte s'ha eliminat correctament.";
            }
            catch (Exception ex)
            {
                return "El contacte no s'ha pogut eliminar.";
            }
        }
    }
}
