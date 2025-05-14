using Disaster_Resource_Allocation_API.Models;
using Disaster_Resource_Allocation_API.Models.Response;

namespace Disaster_Resource_Allocation_API.Services.Interfaces
{
    public interface IAssignmentService
    {
        Task<List<Assignment>> ProcessAndAssignResourcesAsync();
        Task<List<Assignment>?> GetLastAssignmentsAsync();
        Task ClearAssignmentsAsync();
    }
}
