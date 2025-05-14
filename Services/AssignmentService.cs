using Disaster_Resource_Allocation_API.Models;
using Disaster_Resource_Allocation_API.Repositories.Interface;
using Disaster_Resource_Allocation_API.Services.Interfaces;

namespace Disaster_Resource_Allocation_API.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly ITruckRepository _truckRepository;
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentService(
            IAreaRepository areaRepository,
            ITruckRepository truckRepository,
            IAssignmentRepository assignmentRepository)
        {
            _areaRepository = areaRepository;
            _truckRepository = truckRepository;
            _assignmentRepository = assignmentRepository;
        }

        public async Task<List<Assignment>> ProcessAndAssignResourcesAsync()
        {
            var areas = await _areaRepository.GetAllAreasAsync();
            var allTrucks = await _truckRepository.GetAllTrucksAsync();

            var availableTrucks = allTrucks.Select(t => new ResourceTruck
            {
                TruckId = t.TruckId,
                AvailableResources = new Dictionary<string, int>(t.AvailableResources),
                TravelTimeHourToArea = new Dictionary<string, int>(t.TravelTimeHourToArea)
            }).ToList();

            var assignments = new List<Assignment>();
            var assignedAreaIds = new HashSet<string>();
            var assignedTruckIds = new HashSet<string>();

            var sortedAreas = areas
                .OrderByDescending(a => a.UrgencyLevel)
                .ThenBy(a => a.TimeConstraint)
                .ToList();

            foreach (var area in sortedAreas)
            {
                if (assignedAreaIds.Contains(area.AreaId)) continue;

                ResourceTruck? bestTruck = null;
                int bestTruckTravelTime = int.MaxValue;

                Assignment areaAssignment = new Assignment
                {
                    AreaId = area.AreaId
                };

                foreach (var truck in availableTrucks)
                {
                    if (assignedTruckIds.Contains(truck.TruckId)) continue; 

                    if (!truck.TravelTimeHourToArea.TryGetValue(area.AreaId, out var travelTime) || travelTime > area.TimeConstraint)
                    {
                        continue;
                    }

                    areaAssignment.isHasTruckTimeAvailble = true;

                    bool canMeetAllResources = true;
                    foreach (var requiredResource in area.RequiredResources)
                    {
                        if (!truck.AvailableResources.TryGetValue(requiredResource.Key, out var availableAmount) || availableAmount < requiredResource.Value)
                        {
                            canMeetAllResources = false;
                            break;
                        }
                    }

                    if (canMeetAllResources)
                    {
                        if (travelTime <= bestTruckTravelTime)
                        {
                            bestTruck = truck;
                            bestTruckTravelTime = travelTime;
                        }
                    }
                }

                if (bestTruck != null)
                {
                    areaAssignment.isHasTruckResourceAvailble = true;

                    var deliveredResources = new Dictionary<string, int>();
                    foreach (var required in area.RequiredResources)
                    {
                        deliveredResources.Add(required.Key, required.Value);
                        bestTruck.AvailableResources[required.Key] -= required.Value;
                    }

                    areaAssignment.TruckId = bestTruck.TruckId;
                    areaAssignment.ResourcesDelivered = deliveredResources;

                    assignedAreaIds.Add(area.AreaId);
                    assignedTruckIds.Add(bestTruck.TruckId); 
                }

                areaAssignment.Message = GetAssignmentMessage(areaAssignment);
                assignments.Add(areaAssignment);
            }

            await _assignmentRepository.SaveAssignmentsAsync(assignments);
            return assignments;
        }

        public async Task<List<Assignment>?> GetLastAssignmentsAsync()
        {
            var cachedList = await _assignmentRepository.GetLastAssignmentsAsync();
            if (cachedList != null)
            {
                return cachedList;
            }
            return null;
        }

        public async Task ClearAssignmentsAsync()
        {
            await _assignmentRepository.ClearAssignmentsAsync();
        }

        private string GetAssignmentMessage(Assignment data)
        {
            string msg = string.Empty;
            if(!data.isHasTruckTimeAvailble && !data.isHasTruckResourceAvailble)
            {
                msg =  $"No Any Resourced Truck And Timed Truck Availble For This Area"; 
            }
            else if(!data.isHasTruckResourceAvailble)
            {
                msg = $"No Any Resourced Truck Availble For This Area";
            }
            else if (!data.isHasTruckTimeAvailble)
            {
                msg = $"No Any Timed Truck Availble For This Area";
            }
            else
            {
                return null;
            }
            return msg;
        }
    }
}
