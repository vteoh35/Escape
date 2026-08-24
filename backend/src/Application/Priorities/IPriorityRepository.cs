using Business_Logic.Priorities;

namespace Application.Priorities;

public interface IPriorityRepository
{
    List<Priority> GetAll();
    Priority? GetById(int priorityId);
    void Add(Priority priority);
    void Update(Priority priority);
    void Delete(Priority priority);
}
