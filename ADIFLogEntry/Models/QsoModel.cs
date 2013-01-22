using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADIFLogEntry.Models
{
    public sealed class QsoModel
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Band { get; set; }
        public string Mode { get; set; }
        public string Callsign { get; set; }
        public string RstTx { get; set; }
        public string RstRx { get; set; }
    }
}