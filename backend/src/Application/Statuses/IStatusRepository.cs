using Business_Logic.Statuses;

namespace Application.Statuses;

public interface IStatusRepository
{
    List<Status> GetAll();
    Status? GetById(int statusId);
    void Add(Status status);
    void Update(Status status);
    void Delete(Status status);
}
