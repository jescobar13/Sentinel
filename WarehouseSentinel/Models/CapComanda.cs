using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSentinel.Models
{
    public class CapComanda
    {
        public int ComandaID { get; set; }
        public string ClientCIF { get; set; }
        public string ClientNom { get; set; }
        public string Localitat { get; set; }

        public CapComanda(comanda comanda)
        {
            ComandaID = comanda.codi;
            ClientCIF = comanda.client.CIF;
            ClientNom = comanda.client.nom;
            Localitat = " - - - !!"; // client.localitat;
        }
    }
}
