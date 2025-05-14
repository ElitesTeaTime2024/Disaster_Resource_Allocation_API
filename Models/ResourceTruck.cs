namespace Disaster_Resource_Allocation_API.Models
{
    public class ResourceTruck
    {
        public required string TruckId { get; set; } = string.Empty;
        public Dictionary<string, int> AvailableResources { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> TravelTimeHourToArea { get; set; } = new Dictionary<string, int>(); 
    }

}
