using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace MarkDav.Module.Ink.Repository
{
    public class InkContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.Ink> Ink { get; set; }

        public InkContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.Ink>().ToTable(ActiveDatabase.RewriteName("MarkDavInk"));
        }
    }
}
