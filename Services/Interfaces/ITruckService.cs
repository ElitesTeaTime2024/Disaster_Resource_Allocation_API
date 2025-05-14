using Disaster_Resource_Allocation_API.Models;
using Disaster_Resource_Allocation_API.Models.Response;

namespace Disaster_Resource_Allocation_API.Services.Interfaces
{
    public interface ITruckService
    {
        Task<List<ResourceTruck>> CreateTrucksAsync(List<ResourceTruck> truck);
        Task<List<ResourceTruck>> GetAllTrucksAsync();
        Task DeleteAllTrucksAsync();
    }
}
