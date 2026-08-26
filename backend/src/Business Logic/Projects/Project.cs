namespace Business_Logic.Projects;

/// <summary>
/// A project: the top-level container that Tasks belong to.
/// </summary>
public class Project
{
    public string ProjectID {get; set;}
    public string Name {get; set;}
    public string? Description {get; set;}
    public int? PriorityId { get; set; }
    public int? StatusId { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}