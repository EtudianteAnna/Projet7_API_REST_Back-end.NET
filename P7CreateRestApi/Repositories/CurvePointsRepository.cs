using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public class CurvePointRepository : ICurvePointRepository
    {
        private readonly LocalDbContext _context;

        public CurvePointRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CurvePoint>> GetAllAsync()
        {
            IQueryable<CurvePoint> query = _context.CurvePointss;

            // Ajouter la vérification pour éviter CS8604
            switch (query)
            {
                case null:
                    return Enumerable.Empty<CurvePoint>();
                default:
                    return query.ToList();
            }
        }

        public async Task<CurvePoint> GetByIdAsync(int id)
        {
            return await _context.CurvePointss.FindAsync(id);
        }

        public async Task AddAsync(CurvePoint curvePoint)
        {
            _context.CurvePointss.Add(curvePoint);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CurvePoint curvePoint)
        {
            _context.CurvePointss.Update(curvePoint);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var curvePoint = await _context.CurvePointss.FindAsync(id);
            if (curvePoint != null)
            {
                _context.CurvePointss.Remove(curvePoint);
                await _context.SaveChangesAsync();
            }
        }
    }
}
    

    