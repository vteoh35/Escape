namespace Business_Logic.Comments;

/// <summary>
/// A threaded comment on a task. Comments attach to tasks only (not projects), and can reply to another comment via ParentCommentId.
/// </summary>
public class Comment
{
    public string CommentId { get; set; }
    public string? Description { get; set; }
    public string? TaskId { get; set; }
    public string? EmployeeId { get; set; }
    public DateTime? CommentTime { get; set; }
    public string? ParentCommentId { get; set; }
}