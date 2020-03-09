using System;
using System.Collections.Generic;
using System.Text;

namespace AOFileProcessor.Entities
{
    public class ResultEntity
    {
        public int CompId { get; set; }
        public int EventId { get; set; }
        public int AthleteId { get; set; }
        public String Mark { get; set; }
        public int Position { get; set; }
        public String Wind { get; set; }
    }
}
