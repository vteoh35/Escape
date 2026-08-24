namespace Business_Logic.Projects;

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