namespace Application.Statuses;

/// <summary>
/// Deletes a status. Returns false if the id doesn't exist.
/// </summary>
public class DeleteStatus
{
    private readonly IStatusRepository _statusRepository;

    public DeleteStatus(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }

    public bool Execute(int statusId)
    {
        var status = _statusRepository.GetById(statusId);

        if (status == null)
        {
            return false;
        }

        _statusRepository.Delete(status);

        return true;
    }
}
