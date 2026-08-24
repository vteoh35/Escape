namespace Business_Logic.Comments;

public class Comment
{
    public string CommentId { get; set; }
    public string? Description { get; set; }
    public string? TaskId { get; set; }
    public string? EmployeeId { get; set; }
    public DateTime? CommentTime { get; set; }
    public string? ParentCommentId { get; set; }
}