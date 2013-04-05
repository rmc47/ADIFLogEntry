using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADIFLogEntry.Models
{
    public sealed class LogModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Locator { get; set; }
        public string WabSquare { get; set; }
    }
}