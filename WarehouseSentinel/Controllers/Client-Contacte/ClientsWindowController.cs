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
        SentinelDBEntities context;
        TClient tClient;
        TContacte tContacte;
        List<client> memoria;

        public ClientsWindowController()
        {
            context = new SentinelDBEntities();
            tClient = new TClient(context);
            tContacte = new TContacte(context);
        }

        public SentinelDBEntities getBaseContext()
        {
            return context;
        }

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

        internal IEnumerable donemContactes(client clientSeleccionat)
        {
            return tContacte.getByClient(clientSeleccionat);
        }

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
