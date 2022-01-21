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
    public class ProjectImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectImageId { get; set; }

        public int ProjectDetailId { get; set; }

        [ForeignKey("ProjectDetailId")]
        [IgnoreDataMember]
        public virtual ProjectDetail ProjectDetail { get; set; }

        [MaxLength(255)]
        public string ImagePath { get; set; }
    }
}
