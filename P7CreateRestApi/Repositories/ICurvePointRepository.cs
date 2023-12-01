using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface ICurvePointRepository
    {
        Task<CurvePoints> GetByIdAsync(int id);
        Task AddAsync(CurvePoints curvePoint);
        Task UpdateAsync(CurvePoints curvePoint);
        Task DeleteAsync(int id);
    }
}