using Disaster_Resource_Allocation_API.Models;
using Disaster_Resource_Allocation_API.Repositories;
using Disaster_Resource_Allocation_API.Repositories.Interface;
using Disaster_Resource_Allocation_API.Services.Interfaces;

namespace Disaster_Resource_Allocation_API.Services
{
    public class TruckService : ITruckService
    {
        private readonly ITruckRepository _truckRepository;

        public TruckService(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;
        }

        public async Task<List<ResourceTruck>> CreateTrucksAsync(List<ResourceTruck> trucks)
        {
            foreach (var truck in trucks)
            {
                await _truckRepository.AddTruckAsync(truck);
            }
            return trucks;
        }

        public async Task<List<ResourceTruck>> GetAllTrucksAsync()
        {
            return await _truckRepository.GetAllTrucksAsync();
        }

        public async Task DeleteAllTrucksAsync()
        {
            await _truckRepository.DeleteAllTrucksAsync();
        }
    }
}
