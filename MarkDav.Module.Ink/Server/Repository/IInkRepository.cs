using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkDav.Module.Ink.Repository
{
    public interface IInkRepository
    {
        IEnumerable<Models.Ink> GetInks(int ModuleId);
        Models.Ink GetInk(int InkId);
        Models.Ink GetInk(int InkId, bool tracking);
        Models.Ink AddInk(Models.Ink Ink);
        Models.Ink UpdateInk(Models.Ink Ink);
        void DeleteInk(int InkId);
    }
}
