using Disaster_Resource_Allocation_API.Models;
using Disaster_Resource_Allocation_API.Repositories.Interface;

namespace Disaster_Resource_Allocation_API.Repositories
{
    public class TruckRepository : ITruckRepository
    {
        private readonly List<ResourceTruck> _trucks = new List<ResourceTruck>();

        public TruckRepository()
        {

        }

        public Task AddTruckAsync(ResourceTruck truck)
        {
            _trucks.Add(truck);
            return Task.CompletedTask;
        }

        public Task<ResourceTruck?> GetTruckByIdAsync(string truckId)
        {
            return Task.FromResult(_trucks.FirstOrDefault(t => t.TruckId == truckId));
        }

        public Task<List<ResourceTruck>> GetAllTrucksAsync()
        {
            return Task.FromResult(new List<ResourceTruck>(_trucks));
        }

        public Task DeleteAllTrucksAsync()
        {
            _trucks.Clear();
            return Task.CompletedTask;
        }
    }
}
