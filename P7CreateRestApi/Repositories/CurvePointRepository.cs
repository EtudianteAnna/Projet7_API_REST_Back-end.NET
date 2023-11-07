
using Dot.Net.WebApi.Domain;

namespace Dot.Net.WebApi.Repositories
    {
        public interface ICurvePointRepository
        {
            CurvePoint GetCurvePointById(int id);
            IEnumerable<CurvePoint> GetAllCurvePoints();
            CurvePoint AddCurvePoint(CurvePoint curvePoint);
            CurvePoint UpdateCurvePoint(int id, CurvePoint curvePoint);
            bool DeleteCurvePoint(int id);
        }
    }


