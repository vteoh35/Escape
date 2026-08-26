using Business_Logic.Employees;

namespace Application.PositionLevels;

/// <summary>
/// Data access contract for PositionLevels, implemented in Infrastructure against Postgres.
/// </summary>
public interface IPositionLevelRepository
{
    List<PositionLevel> GetAll();
    PositionLevel? GetByLevel(int level);
    void Add(PositionLevel positionLevel);
    void Update(PositionLevel positionLevel);
    void Delete(PositionLevel positionLevel);
}
