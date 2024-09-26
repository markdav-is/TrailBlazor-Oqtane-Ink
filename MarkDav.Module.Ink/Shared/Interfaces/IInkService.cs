using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkDav.Module.Ink.Services
{
    public interface IInkService 
    {
        Task<List<Models.Ink>> GetInksAsync(int ModuleId);

        Task<Models.Ink> GetInkAsync(int InkId, int ModuleId);

        Task<Models.Ink> AddInkAsync(Models.Ink Ink);

        Task<Models.Ink> UpdateInkAsync(Models.Ink Ink);

        Task DeleteInkAsync(int InkId, int ModuleId);
    }
}
