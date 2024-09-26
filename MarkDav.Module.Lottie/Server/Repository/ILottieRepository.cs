using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkDav.Module.Lottie.Repository
{
    public interface ILottieRepository
    {
        IEnumerable<Models.Lottie> GetLotties(int ModuleId);
        Models.Lottie GetLottie(int LottieId);
        Models.Lottie GetLottie(int LottieId, bool tracking);
        Models.Lottie AddLottie(Models.Lottie Lottie);
        Models.Lottie UpdateLottie(Models.Lottie Lottie);
        void DeleteLottie(int LottieId);
    }
}
