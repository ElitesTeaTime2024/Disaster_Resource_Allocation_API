using Disaster_Resource_Allocation_API.Models;

namespace Disaster_Resource_Allocation_API.Services.Interfaces
{
    public interface IAreaService
    {
        Task<List<AffectedArea>> CreateAreasAsync(List<AffectedArea> areas);
        Task<List<AffectedArea>> GetAllAreasAsync();
        Task DeleteAllAreasAsync();
    }
}
