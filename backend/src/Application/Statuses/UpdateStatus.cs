using Business_Logic.Statuses;

namespace Application.Statuses;

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
