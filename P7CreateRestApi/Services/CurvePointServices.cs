using Dot.Net.WebApi.Models;
using Dot.Net.WebApi.Repositories;

namespace Dot.Net.WebApi.Services
{
    public class CurvePointService : ICurvePointService
    {
        private readonly ICurvePointRepository _curvePointRepository;

        public CurvePointService(ICurvePointRepository curvePointRepository)
        {
            _curvePointRepository = curvePointRepository;
        }

        public CurvePoint GetCurvePointById(int id)
        {
            var domainCurvePoint = _curvePointRepository.GetCurvePointById(id);

            // Convertir l'objet domaine en objet modèle
            return ConvertToModel(domainCurvePoint);
        }

        public IEnumerable<CurvePoint> GetAllCurvePoints()
        {
            var domainCurvePoints = _curvePointRepository.GetAllCurvePoints();

            // Convertir les objets domaine en objets modèle
            return domainCurvePoints.Select(ConvertToModel);
        }

        public CurvePoint AddCurvePoint(CurvePoint curvePoint)
        {
            // Implémenter la logique pour ajouter un point de courbe
            curvePoint.CreationDate = DateTime.Now;

            // Convertir l'objet modèle en objet domaine
            var domainCurvePoint = ConvertToDomain(curvePoint);

            // Ajouter l'objet domaine
            var addedDomainCurvePoint = _curvePointRepository.AddCurvePoint((Domain.CurvePoint)domainCurvePoint);

            // Convertir l'objet domaine ajouté en objet modèle
            return ConvertToModel(addedDomainCurvePoint);
        }

        private object ConvertToDomain(CurvePoint curvePoint)
        {
            throw new NotImplementedException();
        }

        // Méthodes d'aide pour la conversion

        private CurvePoint ConvertToModel(Dot.Net.WebApi.Domain.CurvePoint domainCurvePoint)
        {
            // Créer un nouvel objet modèle CurvePoint
            var modelCurvePoint = new CurvePoint();

            // Affecter les propriétés du modèle en fonction des propriétés du domaine
            modelCurvePoint.Id = domainCurvePoint.Id;
            modelCurvePoint.AsOfDate = domainCurvePoint.AsOfDate;
            modelCurvePoint.Term = domainCurvePoint.Term;
            modelCurvePoint.CurvePointValue = domainCurvePoint.CurvePointValue;
            // Assurez-vous de faire de même pour toutes les autres propriétés

            // Retourner l'objet modèle
            return modelCurvePoint;


        }
    }

    public interface ICurvePointService
    {
    }
}

    