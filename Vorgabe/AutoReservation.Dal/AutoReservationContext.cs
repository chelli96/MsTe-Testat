﻿using AutoReservation.Dal.Entities;
using AutoReservation.Dal.Migrations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AutoReservation.Dal
{
    public class AutoReservationContext : DbContext
    {
        public AutoReservationContext()
        {
            // Ensures that the database will be initialized
            Database.Initialize(false);

            // Disable lazy loading
            Configuration.LazyLoadingEnabled = false;

            // ----------------------------------------------------------------------------------------------------
            // Choose one of these three options:

            // Use for real "database first"
            //      - Database will NOT be created by Entity Framework
            //      - Database will NOT be modified by Entity Framework
            // Database.SetInitializer<AutoReservationContext>(null);

            // Use this for initial "code first" 
            //      - Database will be created by Entity Framework
            //      - Database will NOT be modified by Entity Framework
            // Database.SetInitializer(new CreateDatabaseIfNotExists<AutoReservationContext>());

            // Use this for real "code first" 
            //      - Database will be created by Entity Framework
            //      - Database will be modified by Entity Framework
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AutoReservationContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set up hierarchical mapping in fluent API
            //      Remarks:
            //      This could not be done using attributes on business entities
            //      since the discriminator (AutoKlasse) must not be part of the entity.

            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Auto>()
                .Map<StandardAuto>(a => a.Requires("AutoKlasse").HasValue(2))
                .Map<MittelklasseAuto>(a => a.Requires("AutoKlasse").HasValue(1))
                .Map<LuxusklasseAuto>(a => a.Requires("AutoKlasse").HasValue(0))
                .ToTable("Auto");


        }
    }
}
