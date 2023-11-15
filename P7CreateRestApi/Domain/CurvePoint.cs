using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;

namespace P7CreateRestApi.Domain
{
    public class CurvePoint
    {
        // TODO: Map columns in data table CURVEPOINT with corresponding fields

        public int Id { get; set; }

        public byte? CurveId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? AsOfDate { get; set; }

        public double? Term { get; set; }

        public double? CurvePointValue { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreationDate { get; set; }
    }
}


