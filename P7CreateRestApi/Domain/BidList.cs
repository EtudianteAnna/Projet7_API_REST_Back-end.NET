using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Domain
{
    public class BidList
    {
        // TODO: Map columns in data table BIDLIST with corresponding fields


        public class Bid
    {
            public int BidListId { get; set; }

        [Required]
        public string Account { get; set; }

        [Required]
        public string BidType { get; set; }

        public double? BidQuantity { get; set; }
        public double? AskQuantity { get; set; }
            public double? BidAmount { get; set; }
        public double? Ask { get; set; }

        public string Benchmark { get; set; }
        public DateTime? BidListDate { get; set; }
        public string Commentary { get; set; }

        [Required]
        public string BidSecurity { get; set; }

        [Required]
        public string BidStatus { get; set; }
            public string Trader { get; set; }
        public string Book { get; set; }
        public string CreationName { get; set; }
        public DateTime? CreationDate { get; set; }
        public string RevisionName { get; set; }
        public DateTime? RevisionDate { get; set; }
        public string DealName { get; set; }
        public string DealType { get; set; }
        public string SourceListId { get; set; }
        public string Side { get; set; }
    }

}
}
