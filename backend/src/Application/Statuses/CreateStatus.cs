using Business_Logic.Statuses;

namespace Application.Statuses;

/// <summary>
/// Creates a new status. StatusId is manually assigned (not database-generated), so the caller picks it.
/// </summary>
public class CreateStatus
{
    private readonly IStatusRepository _statusRepository;

    public CreateStatus(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }

    public Status Execute(int statusId, string statusName)
    {
        var status = new Status
        {
            StatusId = statusId,
            StatusName = statusName
        };

        _statusRepository.Add(status);

        return status;
    }
}
