namespace WhatupTeam.Models.DatabaseAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using WhatupTeam.Models.Entities;

    public class WhatupTeamDatabaseContext : DbContext
    {
        // Your context has been configured to use a 'DatabaseContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WhatupTeam.Models.DatabaseAccess.DatabaseContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DatabaseContext' 
        // connection string in the application configuration file.
        public WhatupTeamDatabaseContext()
            : base("name=databaseConnectionString")
        {
          // Database.SetInitializer<WhatupTeamDatabaseContext>(new WhatupTeamDatabaseInitializer());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<ProjectTeam> ProjectTeams { get; set; }
        public DbSet<Task> Tasks { get; set;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}