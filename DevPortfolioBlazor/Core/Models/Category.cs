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
        public string Name { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; } 

        public List<Post> Posts { get; set; }
    }
}
