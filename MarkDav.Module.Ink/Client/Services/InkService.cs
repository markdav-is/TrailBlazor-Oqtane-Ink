using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Services;
using Oqtane.Shared;

namespace MarkDav.Module.Ink.Services
{
    public class InkService : ServiceBase, IInkService
    {
        public InkService(IHttpClientFactory http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("Ink");

        public async Task<List<Models.Ink>> GetInksAsync(int ModuleId)
        {
            List<Models.Ink> Inks = await GetJsonAsync<List<Models.Ink>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.Ink>().ToList());
            return Inks.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.Ink> GetInkAsync(int InkId, int ModuleId)
        {
            return await GetJsonAsync<Models.Ink>(CreateAuthorizationPolicyUrl($"{Apiurl}/{InkId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.Ink> AddInkAsync(Models.Ink Ink)
        {
            return await PostJsonAsync<Models.Ink>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, Ink.ModuleId), Ink);
        }

        public async Task<Models.Ink> UpdateInkAsync(Models.Ink Ink)
        {
            return await PutJsonAsync<Models.Ink>(CreateAuthorizationPolicyUrl($"{Apiurl}/{Ink.InkId}", EntityNames.Module, Ink.ModuleId), Ink);
        }

        public async Task DeleteInkAsync(int InkId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{InkId}", EntityNames.Module, ModuleId));
        }
    }
}
