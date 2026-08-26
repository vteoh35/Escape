using Business_Logic.Employees;

namespace Application.PositionLevels;

/// <summary>
/// Creates a new position level. Level is manually assigned (not database-generated), so the caller picks it.
/// </summary>
public class CreatePositionLevel
{
    private readonly IPositionLevelRepository _positionLevelRepository;

    public CreatePositionLevel(IPositionLevelRepository positionLevelRepository)
    {
        _positionLevelRepository = positionLevelRepository;
    }

    public PositionLevel Execute(int level, string? position)
    {
        var positionLevel = new PositionLevel
        {
            Level = level,
            Position = position
        };

        _positionLevelRepository.Add(positionLevel);

        return positionLevel;
    }
}
