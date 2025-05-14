namespace Disaster_Resource_Allocation_API.Models
{
    public class AffectedArea
    {
        public required string AreaId { get; set; } = string.Empty;
        public int UrgencyLevel { get; set; } 
        public Dictionary<string, int> RequiredResources { get; set; } = new Dictionary<string, int>();
        public int TimeConstraint { get; set; }
    }
}
