
using Dot.Net.WebApi.Domain;

namespace Dot.Net.WebApi.Repositories
{
    public interface ICurvePointRepository
    {

        Task<IEnumerable<CurvePoint>> GetAllAsync();
        Task<CurvePoint> GetByIdAsync(int id);
        Task AddAsync(CurvePoint curvePoint);
        Task UpdateAsync(CurvePoint curvePoint);
        Task DeleteAsync(int id);
    }
}

