using Disaster_Resource_Allocation_API.Models.Response;
using Disaster_Resource_Allocation_API.Models;
using Disaster_Resource_Allocation_API.Repositories.Interface;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using StackExchange.Redis;

namespace Disaster_Resource_Allocation_API.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly IDatabase _redisDatabase;
        private const string AssignmentCacheKey = "LastProcessAssignment";

        public AssignmentRepository(IConnectionMultiplexer redisConnection)
        {
            _redisDatabase = redisConnection.GetDatabase();
        }

        public async Task<List<Assignment>?> GetLastAssignmentsAsync()
        {
            var cachedAssignments = await _redisDatabase.StringGetAsync(AssignmentCacheKey);
            if (string.IsNullOrEmpty(cachedAssignments))
            {
                return null;
            }
            return JsonSerializer.Deserialize<List<Assignment>>(cachedAssignments);
        }

        public async Task SaveAssignmentsAsync(List<Assignment> assignments)
        {
            var serializedAssignments = JsonSerializer.Serialize(assignments);
            await _redisDatabase.StringSetAsync(AssignmentCacheKey, serializedAssignments, TimeSpan.FromMinutes(30));
        }

        public async Task ClearAssignmentsAsync()
        {
            await Task.FromResult(_redisDatabase.KeyDelete(AssignmentCacheKey));
        }
    }
}
