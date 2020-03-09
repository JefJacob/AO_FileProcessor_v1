using System;
using System.Collections.Generic;
using System.Text;

namespace AOFileProcessor.Entities
{
    public class CompetitionEntity
    {
        public int CompId { get; set; }
        public String Name { get; set; }
        public String SubName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Facility { get; set; }
        public String Location { get; set; }
        public String MeetType { get; set; }
        public String MeetSubType { get; set; }
        public String Season { get; set; }
        

    }
}
