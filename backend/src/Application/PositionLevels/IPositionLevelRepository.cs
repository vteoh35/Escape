using Business_Logic.Employees;

namespace Application.PositionLevels;

public interface IPositionLevelRepository
{
    List<PositionLevel> GetAll();
    PositionLevel? GetByLevel(int level);
    void Add(PositionLevel positionLevel);
    void Update(PositionLevel positionLevel);
    void Delete(PositionLevel positionLevel);
}
