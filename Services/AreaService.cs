using Disaster_Resource_Allocation_API.Models;
using Disaster_Resource_Allocation_API.Repositories.Interface;
using Disaster_Resource_Allocation_API.Services.Interfaces;

namespace Disaster_Resource_Allocation_API.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;

        public AreaService(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public async Task<List<AffectedArea>> CreateAreasAsync(List<AffectedArea> areas)
        {
            foreach(var area in areas)
            {
                if (area.UrgencyLevel < 1 || area.UrgencyLevel > 5)
                {
                    throw new ArgumentException("Urgency level must be between 1 and 5.");
                }
                await _areaRepository.AddAreaAsync(area);
            }
            return areas;
        }

        public async Task<List<AffectedArea>> GetAllAreasAsync()
        {
            return await _areaRepository.GetAllAreasAsync();
        }

        public async Task DeleteAllAreasAsync()
        {
            await _areaRepository.DeleteAllAreasAsync();
        }
    }
}
