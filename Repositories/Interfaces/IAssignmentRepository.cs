using Disaster_Resource_Allocation_API.Models;
using Disaster_Resource_Allocation_API.Models.Response;

namespace Disaster_Resource_Allocation_API.Repositories.Interface
{
    public interface IAssignmentRepository
    {
        Task<List<Assignment>?> GetLastAssignmentsAsync();
        Task SaveAssignmentsAsync(List<Assignment> assignments);
        Task ClearAssignmentsAsync();
    }
}
