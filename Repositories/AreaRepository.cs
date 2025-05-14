using Disaster_Resource_Allocation_API.Models;
using Disaster_Resource_Allocation_API.Repositories.Interface;

namespace Disaster_Resource_Allocation_API.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly List<AffectedArea> _areas = new List<AffectedArea>();

        public AreaRepository()
        {

        }

        public Task AddAreaAsync(AffectedArea area)
        {
            _areas.Add(area);
            return Task.CompletedTask;
        }

        public Task<AffectedArea?> GetAreaByIdAsync(string areaId)
        {
            return Task.FromResult(_areas.FirstOrDefault(a => a.AreaId == areaId));
        }

        public Task<List<AffectedArea>> GetAllAreasAsync()
        {
            return Task.FromResult(new List<AffectedArea>(_areas));
        }

        public Task DeleteAllAreasAsync()
        {
            _areas.Clear();
            return Task.CompletedTask;
        }
    }
}
