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
    
    public partial class comanda
    {
        public comanda()
        {
            this.albara = new HashSet<albara>();
            this.liniacomanda = new HashSet<liniacomanda>();
        }
    
        public int codi { get; set; }
        public Nullable<System.DateTime> dataComanda { get; set; }
        public Nullable<System.DateTime> dataEntrega { get; set; }
        public string Client_CIF { get; set; }
        public int Client_Contacte_id { get; set; }
        public string estat { get; set; }
    
        public virtual ICollection<albara> albara { get; set; }
        public virtual client client { get; set; }
        public virtual ICollection<liniacomanda> liniacomanda { get; set; }
    }
}
