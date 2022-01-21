using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }


        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required]
        [MaxLength(256)]
        public string ThumbnailImagePath { get; set; }

        [Required]
        [MaxLength(512)]
        public string Excerpt { get; set; }

        [Required]
        [MaxLength(65536)]
        public string Content { get; set; }

        [Required]
        [MaxLength(32)]
        public string PublishDate { get; set; }

        [Required]
        public bool Published { get; set; }

        [Required]
        [MaxLength(128)]
        public string Author { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [IgnoreDataMember]
        public Category Category { get; set; }
    }
}
