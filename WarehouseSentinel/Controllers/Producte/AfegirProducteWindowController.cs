using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSentinel.Models;

namespace WarehouseSentinel.Controllers.Producte
{
    public class AfegirProducteWindowController
    {
        private SentinelDBEntities context;
        private TProducte tProducte;

        public AfegirProducteWindowController(SentinelDBEntities context)
        {
            this.context = context;
            tProducte = new TProducte(context);
        }

        internal string afegeix(producte producte)
        {
            try
            {
                tProducte.add(producte);
                return "El producte " + producte.nom + " s'ha guardat correctament.";
            }
            catch (Exception ex)
            {
                return "El producte " + producte.nom + " no s'ha pogut guardar correctament.";
            }
        }

        internal string modifica(producte producte)
        {
            try
            {
                tProducte.modify(producte);
                return "El producte " + producte.nom + " s'ha modificat correctament.";
            }
            catch (Exception ex)
            {
                return "El producte " + producte.nom + " no s'ha pogut modificar correctament.";
            }
        }
    }
}
