using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSentinel.Models;
using WarehouseSentinel.Viwers.Comanda;

namespace WarehouseSentinel.Controllers
{
    public class GestorComandesWindowController
    {
        private SentinelDBEntities context;
        private TComanda tComanda;
        private TLiniaComanda tLiniaComanda;

        public GestorComandesWindowController()
        {
            context = new SentinelDBEntities();
            tComanda = new TComanda(context);
            tLiniaComanda = new TLiniaComanda(context);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SentinelDBEntities getBaseContext()
        {
            return context;
        }

        internal IEnumerable donemComandes()
        {
            return tComanda.getAll();
        }

        internal IEnumerable donemComandesByCif(string cif)
        {
            cif = "S3959337A";

            return tComanda.getByCIFClient(cif);
        }
    }
}
