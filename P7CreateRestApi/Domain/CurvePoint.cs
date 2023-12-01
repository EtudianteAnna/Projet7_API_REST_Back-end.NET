﻿using System.ComponentModel.DataAnnotations;


namespace P7CreateRestApi.Domain
{
    public class CurvePoints
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


