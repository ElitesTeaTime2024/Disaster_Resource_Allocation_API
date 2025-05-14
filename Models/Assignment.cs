using System.Text.Json.Serialization;

namespace Disaster_Resource_Allocation_API.Models
{
    public class Assignment
    {
        public string AreaId { get; set; } = string.Empty;

        [JsonIgnore]
        public bool isHasTruckResourceAvailble { get; set; } = false;

        [JsonIgnore]
        public bool isHasTruckTimeAvailble { get; set; } = false;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? TruckId { get; set; } 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Dictionary<string, int>? ResourcesDelivered { get; set; } 
    }
}
