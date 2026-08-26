using Business_Logic.Priorities;

namespace Application.Priorities;

/// <summary>
/// Creates a new priority. PriorityId is manually assigned (not database-generated), so the caller picks it.
/// </summary>
public class CreatePriority
{
    private readonly IPriorityRepository _priorityRepository;

    public CreatePriority(IPriorityRepository priorityRepository)
    {
        _priorityRepository = priorityRepository;
    }

    public Priority Execute(int priorityId, string priorityName)
    {
        var priority = new Priority
        {
            PriorityId = priorityId,
            PriorityName = priorityName
        };

        _priorityRepository.Add(priority);

        return priority;
    }
}
