using Business_Logic.Employees;

namespace Application.PositionLevels;

/// <summary>
/// Updates a position level's name. Returns null if the level doesn't exist.
/// </summary>
public class UpdatePositionLevel
{
    private readonly IPositionLevelRepository _positionLevelRepository;

    public UpdatePositionLevel(IPositionLevelRepository positionLevelRepository)
    {
        _positionLevelRepository = positionLevelRepository;
    }

    public PositionLevel? Execute(int level, string? position)
    {
        var positionLevel = _positionLevelRepository.GetByLevel(level);

        if (positionLevel == null)
        {
            return null;
        }

        positionLevel.Position = position;

        _positionLevelRepository.Update(positionLevel);

        return positionLevel;
    }
}
