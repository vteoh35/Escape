using Business_Logic.Statuses;

namespace Application.Statuses;

/// <summary>
/// Updates a status's name. Returns null if the id doesn't exist.
/// </summary>
public class UpdateStatus
{
    private readonly IStatusRepository _statusRepository;

    public UpdateStatus(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }

    public Status? Execute(int statusId, string statusName)
    {
        var status = _statusRepository.GetById(statusId);

        if (status == null)
        {
            return null;
        }

        status.StatusName = statusName;

        _statusRepository.Update(status);

        return status;
    }
}
