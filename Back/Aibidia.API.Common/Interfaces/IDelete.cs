using System.Threading.Tasks;

namespace Aibidia.API.Common.Interfaces
{
    public interface IDelete<TModel, TKey>
    {
        Task DeleteAsync(TKey obj);
    }
}
