using System;

namespace Aeiges.Models
{
    public class Complaint
    {
        public int ComplaintID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? CrimeTypeID { get; set; }
        public string CrimeTypeName { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string EvidencePath { get; set; }
        public int? AssignedOfficerID { get; set; }
        public string AssignedOfficerName { get; set; }
        public string Location { get; set; }
        public DateTime? IncidentDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ReporterName { get; set; }
    }
}
