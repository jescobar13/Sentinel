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
    
    public partial class log_in
    {
        public log_in()
        {
            this.registre = new HashSet<registre>();
        }
    
        public int id { get; set; }
        public string usuari { get; set; }
        public string password { get; set; }
        public string permisos { get; set; }
    
        public virtual ICollection<registre> registre { get; set; }
    }
}
