using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace MarkDav.Module.Lottie.Repository
{
    public class LottieContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.Lottie> Lottie { get; set; }

        public LottieContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.Lottie>().ToTable(ActiveDatabase.RewriteName("MarkDavLottie"));
        }
    }
}
