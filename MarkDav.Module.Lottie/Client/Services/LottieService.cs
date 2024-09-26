using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Services;
using Oqtane.Shared;

namespace MarkDav.Module.Lottie.Services
{
    public class LottieService : ServiceBase, ILottieService
    {
        public LottieService(IHttpClientFactory http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("Lottie");

        public async Task<List<Models.Lottie>> GetLottiesAsync(int ModuleId)
        {
            List<Models.Lottie> Lotties = await GetJsonAsync<List<Models.Lottie>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.Lottie>().ToList());
            return Lotties.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.Lottie> GetLottieAsync(int LottieId, int ModuleId)
        {
            return await GetJsonAsync<Models.Lottie>(CreateAuthorizationPolicyUrl($"{Apiurl}/{LottieId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.Lottie> AddLottieAsync(Models.Lottie Lottie)
        {
            return await PostJsonAsync<Models.Lottie>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, Lottie.ModuleId), Lottie);
        }

        public async Task<Models.Lottie> UpdateLottieAsync(Models.Lottie Lottie)
        {
            return await PutJsonAsync<Models.Lottie>(CreateAuthorizationPolicyUrl($"{Apiurl}/{Lottie.LottieId}", EntityNames.Module, Lottie.ModuleId), Lottie);
        }

        public async Task DeleteLottieAsync(int LottieId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{LottieId}", EntityNames.Module, ModuleId));
        }
    }
}
