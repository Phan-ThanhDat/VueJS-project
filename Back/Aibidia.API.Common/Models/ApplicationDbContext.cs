using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Aibidia.API.Common.Models.Interfaces;

namespace Aibidia.API.Common.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public DbSet<Resume> Resumes { get; set; }

        public override Task<int> SaveChangesAsync()
        {
            // If a property of a non-change-tracking entity is set directly (eg. obj.SomeProperty = true)
            // the EntityState of the entity will be Unchanged until base.SaveChanges is called.
            // The value is still saved to the database because base.SaveChanges probably calls DetectChanges
            // under the hood.
            // We only need to know beforehand if any items have changed if they implement IUpdated so that we
            // can se the LastUpdated property. (IUpdatedBy inherits from IUpdated so that is also handled.)
            if (this.ChangeTracker.Entries<IUpdated>().Any())
            {
                this.ChangeTracker.DetectChanges();
            }

            // Find entries whose state is either added or modified (eg. at least one bit must be the same as in Added or Modified).
            IEnumerable<DbEntityEntry> upserted = this.ChangeTracker.Entries().Where(e => (e.State & (EntityState.Added | EntityState.Modified)) != 0);
            HandleUpdatedEntities(upserted);

            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        private void HandleUpdatedEntities(IEnumerable<DbEntityEntry> entries)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            string userId = "demo";

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added && entry.Entity is ICreatedAt createdEntity)
                {
                    createdEntity.CreatedAt = now;

                    if (entry.Entity is ICreatedBy createdByEntity)
                    {
                        createdByEntity.CreatedBy = userId;
                    }
                }
                else if (entry.State == EntityState.Modified && entry.Entity is IUpdated updatedEntity)
                {
                    updatedEntity.LastUpdated = now;

                    if (entry.Entity is IUpdatedBy updatedByEntity)
                    {
                        updatedByEntity.LastUpdatedBy = userId;
                    }
                }
            }
        }
    }

    public class ApplicationDbConfig : DbConfiguration
    {
        public ApplicationDbConfig()
        {
            // Sets up EF to use the SQL Azure execution strategy - to automatically retry failed database operations
            // https://docs.microsoft.com/en-us/ef/ef6/fundamentals/configuring/code-based
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            //AddInterceptor(new SessionContextInterceptor());
        }
    }
}