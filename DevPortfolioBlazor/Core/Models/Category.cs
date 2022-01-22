using Core.Validation;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; } 

        [Required]
        [MaxLength(256)]
        public string ThumbnailPath { get; set; }

        [Required]
        [MaxLength(128)]
        [NoPeriods(ErrorMessage = "The category Name field contains one or more period characters (.). Please remove all periods.")]
        [NoThreeSpaces(ErrorMessage = "The category Name field contains three or more spaces in a row. Please remove them.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; } 

        public List<Post> Posts { get; set; }
    }
}
