//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WarehouseSentinel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class liniaalbara
    {
        public int id { get; set; }
        public Nullable<float> pes { get; set; }
        public string producteNom { get; set; }
        public int Albara_codi { get; set; }
        public int Albara_Comanda_codi { get; set; }
        public string Albara_Comanda_Client_CIF { get; set; }
        public int Albara_Comanda_Client_Contacte_id { get; set; }
        public Nullable<int> caixa { get; set; }
        public Nullable<int> idProducte { get; set; }
    
        public virtual albara albara { get; set; }
    }
}
