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
    public enum FiltratPer
    {
        CIF, NOM, COGNOM, PAIS, CODIPOSTAL, ESTAT
    }

    public enum ModeVisualitzacioClients
    {
        ACTIUS, DESHABILITATS, TOTS
    }

    public class ClientsWindowController
    {
        /// <summary>
        /// Context de la base de dades.
        /// </summary>
        SentinelDBEntities context;
        /// <summary>
        /// Referencia a la taula Client.
        /// </summary>
        TClient tClient;
        /// <summary>
        /// Referencia a la taula Contacte.
        /// </summary>
        TContacte tContacte;

        /// <summary>
        /// Llista de clients que actua com a memoria per no fer la consulta
        /// tantes vegades.
        /// </summary>
        List<client> memoria;

        /// <summary>
        /// Constructor. Controlador de la vista Client Window.
        /// </summary>
        public ClientsWindowController()
        {
            context = new SentinelDBEntities();
            tClient = new TClient(context);
            tContacte = new TContacte(context);
        }

        /// <summary>
        /// Serveix el contexte de la base de dades.
        /// </summary>
        /// <returns></returns>
        public SentinelDBEntities getBaseContext()
        {
            return context;
        }

        /// <summary>
        /// Llista els clients de la base de dades segons un mode
        /// </summary>
        /// <param name="mode">ACTIU / DESHABILITATS / TOTS</param>
        /// <returns>Llista de clients.</returns>
        internal IEnumerable donemClients(ModeVisualitzacioClients mode)
        {
            if (memoria != null)
                memoria.Clear();

            switch (mode)
            {
                case ModeVisualitzacioClients.ACTIUS:
                    memoria = tClient.getClientsActius();
                    return memoria;
                    break;

                case ModeVisualitzacioClients.DESHABILITATS:
                    memoria = tClient.getClientsDeshabilitats();
                    return memoria;
                    break;

                case ModeVisualitzacioClients.TOTS:
                    memoria = tClient.getAll();
                    return memoria;
                    break;
            }

            return null;
        }

        /// <summary>
        /// Llista contactes segons un client.
        /// </summary>
        /// <param name="clientSeleccionat">obj. client</param>
        /// <returns>Llista de contactes</returns>
        internal IEnumerable donemContactes(client clientSeleccionat)
        {
            return tContacte.getByClient(clientSeleccionat);
        }

        /// <summary>
        /// Serveix els clients que coincideixen amb el patro passat.
        /// </summary>
        /// <param name="text">patro</param>
        /// <param name="filtra">Filtrat per CIF / NOM</param>
        /// <returns>Llista de Clients</returns>
        internal IEnumerable donemClientsByPattern(string text, FiltratPer filtra)
        {
            Regex searchTerm;
            IEnumerable<client> coinciden;

            switch (filtra)
            {
                case FiltratPer.NOM:
                    searchTerm = new Regex("(" + text + ")|" + text + "([a-z]|[A-Z])");

                    coinciden = (from v in memoria
                                 where searchTerm.Matches(v.nom).Count > 0
                                 select v);

                    return coinciden;
                    break;

                case FiltratPer.CIF:
                    searchTerm = new Regex("(" + text + ")|" + text + "([a-z]|[A-Z]|[0-9])");

                    coinciden = (from v in memoria
                                 where searchTerm.Matches(v.CIF).Count > 0
                                 select v);

                    return coinciden;
                    break;
            }
            return null;
        }

        /// <summary>
        /// Deshabilita un client.
        /// </summary>
        /// <param name="c">obj. client</param>
        /// <returns>Missatge de retorn.</returns>
        internal string deshabilitaClient(client c)
        {
            try
            {
                c.actiu = false;
                tClient.modify(c);
                return "El client " + c.nom + @" s'ha deshabilitat correctament.
Recorda que pots visualitzar-lo canviant el filtre";
            }
            catch (Exception ex)
            {
                return "El client " + c.nom + " no s'ha pogut deshabilitat.";
            }
            throw new NotImplementedException();
        }
    }
}
