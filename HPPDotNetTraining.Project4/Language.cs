using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPPDotNetTraining.Project4
{
    public class Language
    {
        public int Id { get; set; }
        public string LanguageName { get; set; } // Renamed property to avoid conflict with class name
        public string Title { get; set; }
        public string Symbol { get; set; }
    }

}
