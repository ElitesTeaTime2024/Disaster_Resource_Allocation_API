using Disaster_Resource_Allocation_API.Models;

namespace Disaster_Resource_Allocation_API.Repositories.Interface
{
    public interface IAreaRepository
    {
        Task AddAreaAsync(AffectedArea area);
        Task<AffectedArea?> GetAreaByIdAsync(string areaId);
        Task<List<AffectedArea>> GetAllAreasAsync();
        Task DeleteAllAreasAsync();
    }
}
