using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        [MaxLength(255)]
        public string ImagePath { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }

        public virtual ProjectDetail Detail { get; set; }

        //This really should be in a view model
        [NotMapped]
        public string Excerpt
        {
            get
            {
                if (string.IsNullOrEmpty(Description))
                    return "No description specified. A mystery project this is!";

                return Description[..(Description.Length > 200 ? 200 : Description.Length)];
            }
        }
    }
}
