using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace P7CreateRestApi.Domain
{
    public class Rating
    {
        // TODO: Map columns in data table RATING with corresponding fields

        public int Id { get; set; }

        [Required]
        public string? MoodysRating { get; set; }
        [Required]
        public string? SandPRating { get; set; }

        [Required]
        public string? FitchRating { get; set; }

        public byte? OrderNumber { get; set; }

        public Rating()
        {
            MoodysRating = string.Empty;
        }


    }
}
