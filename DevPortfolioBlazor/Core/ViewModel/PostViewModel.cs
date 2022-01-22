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
    public class PostViewModel
    {
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

        [MaxLength(65536)]
        public string Content { get; set; }


        [Required]
        public bool Published { get; set; }

        [Required]
        [MaxLength(128)]
        public string Author { get; set; }

        [Required]
        public int CategoryId { get; set; }

    }
}
