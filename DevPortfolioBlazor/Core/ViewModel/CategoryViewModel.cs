using Core.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class CategoryViewModel
    {
        [Required]
        public int CategoryId { get; set; }

        public string ThumbnailPath { get; set; }

        [Required]
        [MaxLength(128)]
        [NoPeriods(ErrorMessage = "The category Name field contains one or more period characters (.). Please remove all periods.")]
        [NoThreeSpaces(ErrorMessage = "The category Name field contains three or more spaces in a row. Please remove them.")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
