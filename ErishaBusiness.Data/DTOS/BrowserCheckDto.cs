using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ErishaBusiness.Data.DTOS
{
    public class BrowserCheckDto
    {
        public string userAgent { get; set; }
        public Regex OS { get; set; }
        public Regex device { get; set; }
    }
}
