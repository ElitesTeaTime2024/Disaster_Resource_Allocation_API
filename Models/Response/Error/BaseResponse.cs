using System.Text.Json.Serialization;

namespace Disaster_Resource_Allocation_API.Models.Response.Error
{
    public class ErrorResponse
    {
        public int errorStatus { get; set; } = 500;
        public string errorMsg { get; set; } = string.Empty;
    }
}
