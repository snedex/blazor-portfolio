using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    /// <summary>
    /// Represents a skill 
    /// </summary>
    public class Skill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SkillId { get; set; }

        [Range(0, 100)]
        [Required]
        public int DisplayOrder { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string ImagePath { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }
    }
}
