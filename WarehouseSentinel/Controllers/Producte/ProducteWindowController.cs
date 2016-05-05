using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSentinel.Models;

namespace WarehouseSentinel.Controllers.Producte
{
    public enum modeControllerProducte { AFEGIR, MODIFICAR }

    public class ProducteWindowController
    {
        private SentinelDBEntities context;
        private TProducte tProducte;

        public ProducteWindowController()
        {
            context = new SentinelDBEntities();
            tProducte = new TProducte(context);
        }

        public SentinelDBEntities getBaseContext()
        {
            return context;
        }

        internal IEnumerable donemProductes()
        {
            return tProducte.getAll();
        }
    }
}
