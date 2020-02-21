using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CfAppTestSuite.OutboundConnection.Models
{
    public class HttpConnectionResult
    {
        public string Content { get; set; }
        public bool Successful { get; set; }
        public Exception Exception { get; set; }
    }
}
