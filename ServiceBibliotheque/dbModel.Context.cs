﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceBibliotheque
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class biblioEntities : DbContext
    {
        public biblioEntities()
            : base("name=biblioEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Auteur> Auteur { get; set; }
        public DbSet<Categorie> Categorie { get; set; }
        public DbSet<Livre> Livre { get; set; }
        public DbSet<Pret> Pret { get; set; }
        public DbSet<Utilisateur> Utilisateur { get; set; }
    }
}
