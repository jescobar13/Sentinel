﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SentinelDBEntities : DbContext
    {
        public SentinelDBEntities()
            : base("name=SentinelDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<albara> albara { get; set; }
        public DbSet<client> client { get; set; }
        public DbSet<comanda> comanda { get; set; }
        public DbSet<contacte> contacte { get; set; }
        public DbSet<liniaalbara> liniaalbara { get; set; }
        public DbSet<liniacomanda> liniacomanda { get; set; }
        public DbSet<producte> producte { get; set; }
    }
}
