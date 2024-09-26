using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;

namespace MarkDav.Module.Lottie.Repository
{
    public class LottieRepository : ILottieRepository, ITransientService
    {
        private readonly IDbContextFactory<LottieContext> _factory;

        public LottieRepository(IDbContextFactory<LottieContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.Lottie> GetLotties(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.Lottie.Where(item => item.ModuleId == ModuleId).ToList();
        }

        public Models.Lottie GetLottie(int LottieId)
        {
            return GetLottie(LottieId, true);
        }

        public Models.Lottie GetLottie(int LottieId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.Lottie.Find(LottieId);
            }
            else
            {
                return db.Lottie.AsNoTracking().FirstOrDefault(item => item.LottieId == LottieId);
            }
        }

        public Models.Lottie AddLottie(Models.Lottie Lottie)
        {
            using var db = _factory.CreateDbContext();
            db.Lottie.Add(Lottie);
            db.SaveChanges();
            return Lottie;
        }

        public Models.Lottie UpdateLottie(Models.Lottie Lottie)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(Lottie).State = EntityState.Modified;
            db.SaveChanges();
            return Lottie;
        }

        public void DeleteLottie(int LottieId)
        {
            using var db = _factory.CreateDbContext();
            Models.Lottie Lottie = db.Lottie.Find(LottieId);
            db.Lottie.Remove(Lottie);
            db.SaveChanges();
        }
    }
}
