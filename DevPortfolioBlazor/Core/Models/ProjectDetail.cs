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
    public class ProjectDetail
    { 

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectDetailId { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        [IgnoreDataMember]
        public virtual Project Project { get; set; }

        public virtual ICollection<ProjectImage> Images { get; set; }

        public string Content { get; set; }

        [Required]
        [MaxLength(1024)]
        public string SourceLocation { get; set; }

        [Required]
        [MaxLength(1024)]
        public string DemoLocation { get; set; }

    }
}
