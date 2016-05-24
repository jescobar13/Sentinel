using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSentinel.Models
{
    public class TLog_In
    {
        /// <summary>
        /// Context de la base de dades.
        /// </summary>
        private SentinelDBEntities context;

        /// <summary>
        /// Constructor de la base de dades.
        /// </summary>
        /// <param name="context">Contexte de la base de dades.</param>
        public TLog_In(SentinelDBEntities context)
        {
            this.context = context;
        }

        internal log_in getUser(string user)
        {
            return (from tLogIn in context.log_in
                    where tLogIn.usuari.Equals(user)
                    select tLogIn).FirstOrDefault();
        }
    }
}
