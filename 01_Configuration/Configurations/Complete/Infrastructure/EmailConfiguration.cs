using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Complete.Infrastructure
{
    public class EmailConfiguration
    {
        public string Server { get; set; }
        public string ServerUsername { get; set; }
        public string ServerPassword { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
    }
}
