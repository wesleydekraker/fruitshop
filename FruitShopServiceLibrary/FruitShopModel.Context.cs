﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FruitShopServiceLibrary
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FruitShopModelContainer : DbContext
    {
        public FruitShopModelContainer()
            : base("name=FruitShopModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customer> CustomerSet { get; set; }
        public virtual DbSet<OwnedProduct> OwnedProductSet { get; set; }
        public virtual DbSet<Product> ProductSet { get; set; }
    }
}
