using Disaster_Resource_Allocation_API.Models;

namespace Disaster_Resource_Allocation_API.Repositories.Interface
{
    public interface ITruckRepository
    {
        Task AddTruckAsync(ResourceTruck truck);
        Task<ResourceTruck?> GetTruckByIdAsync(string truckId);
        Task<List<ResourceTruck>> GetAllTrucksAsync();
        Task DeleteAllTrucksAsync();
    }
}
