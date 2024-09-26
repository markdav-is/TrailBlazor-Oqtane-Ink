using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;

namespace MarkDav.Module.Ink.Repository
{
    public class InkRepository : IInkRepository, ITransientService
    {
        private readonly IDbContextFactory<InkContext> _factory;

        public InkRepository(IDbContextFactory<InkContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.Ink> GetInks(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.Ink.Where(item => item.ModuleId == ModuleId).ToList();
        }

        public Models.Ink GetInk(int InkId)
        {
            return GetInk(InkId, true);
        }

        public Models.Ink GetInk(int InkId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.Ink.Find(InkId);
            }
            else
            {
                return db.Ink.AsNoTracking().FirstOrDefault(item => item.InkId == InkId);
            }
        }

        public Models.Ink AddInk(Models.Ink Ink)
        {
            using var db = _factory.CreateDbContext();
            db.Ink.Add(Ink);
            db.SaveChanges();
            return Ink;
        }

        public Models.Ink UpdateInk(Models.Ink Ink)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(Ink).State = EntityState.Modified;
            db.SaveChanges();
            return Ink;
        }

        public void DeleteInk(int InkId)
        {
            using var db = _factory.CreateDbContext();
            Models.Ink Ink = db.Ink.Find(InkId);
            db.Ink.Remove(Ink);
            db.SaveChanges();
        }
    }
}
