using System;
using System.Collections.Generic;
using System.Text;

namespace AOFileProcessor.Entities
{
    public class CompetitionEntity
    {
        public int CompId { get; set; }
        public String CompName { get; set; }
        public String CompSubName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Facility { get; set; }
        public String Location { get; set; }
        public String CompType { get; set; }
        public String CompSubType { get; set; }
        public String Season { get; set; }
        

    }
}
