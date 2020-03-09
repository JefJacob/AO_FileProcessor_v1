using System;
using System.Collections.Generic;
using System.Text;

namespace AOFileProcessor.Entities
{
    public class AthleteEntity
    {
        public int AthleteId { get; set; }
        public String ACNum { get; set; }
        public String Fname { get; set; }
        public String Lname { get; set; }
        public DateTime DOB { get; set; }
        public String Gender { get; set; }
        public String ClubCode { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String HeadShot { get; set; }
        public int AthleteSpecialNoteid { get; set; }
        public DateTime ClubAffiliationSince { get; set; }
    }
}
