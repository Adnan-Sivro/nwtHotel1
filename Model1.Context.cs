﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelNWT
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class masterEntities : DbContext
    {
        public masterEntities()
            : base("name=masterEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<food_menu> food_menu { get; set; }
        public DbSet<food_order> food_order { get; set; }
        public DbSet<image> image { get; set; }
        public DbSet<keycard> keycard { get; set; }
        public DbSet<location> location { get; set; }
        public DbSet<reservation> reservation { get; set; }
        public DbSet<resource> resource { get; set; }
        public DbSet<resource_type> resource_type { get; set; }
        public DbSet<user> user { get; set; }
        public DbSet<MSreplication_options> MSreplication_options { get; set; }
        public DbSet<spt_fallback_db> spt_fallback_db { get; set; }
        public DbSet<spt_fallback_dev> spt_fallback_dev { get; set; }
        public DbSet<spt_fallback_usg> spt_fallback_usg { get; set; }
        public DbSet<spt_monitor> spt_monitor { get; set; }
        public DbSet<spt_values> spt_values { get; set; }
    
        public virtual int sp_MScleanupmergepublisher()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_MScleanupmergepublisher");
        }
    
        public virtual int sp_MSrepl_startup()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_MSrepl_startup");
        }
    }
}