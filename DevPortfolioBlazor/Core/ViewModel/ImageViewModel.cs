using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class ImageViewModel
    {
        public string FileExtension { get; set; }

        public string Base64Content { get; set; }

        public string OldImagePath { get; set; }
    }
}
