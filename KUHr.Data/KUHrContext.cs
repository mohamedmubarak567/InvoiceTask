
using KUHr.Data.Entities.InvoiceTables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KUHr.Data
{
    public class KUHrContext : DbContext
    {
        public KUHrContext(DbContextOptions<KUHrContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {        
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Invoice> Invoices { get; set; }
      


        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) => await SetRequiredPropertyWhenSave(null, cancellationToken);

        private async Task<int> SetRequiredPropertyWhenSave(DateTime? date, CancellationToken cancellationToken)
        {
        var PropertiesList = new List<string>() {  "CreatedDate", "ModifiedDate" };

        var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged) continue;
                
                foreach (var property in entry.Properties.Where(q => PropertiesList.Contains(q.Metadata.Name)))
                {
                    var propertyName = property.Metadata.Name;
                    property.CurrentValue = entry.State switch
                    {
                        EntityState.Added => (propertyName switch
                        {
                            "CreatedDate" => date == null ? DateTime.UtcNow : date,
                            _ => property.CurrentValue
                        }),
                        EntityState.Modified => (propertyName switch
                        {
                            "ModifiedDate" => date == null ? DateTime.UtcNow : date,
                            _ => property.CurrentValue
                        }),
                        _ => property.CurrentValue
                    };
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
