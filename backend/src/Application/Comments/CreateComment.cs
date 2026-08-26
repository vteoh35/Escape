using Business_Logic.Comments;

namespace Application.Comments;

/// <summary>
/// Creates a new comment on a task, optionally as a reply to another comment.
/// </summary>
public class CreateComment
{
    private readonly ICommentRepository _commentRepository;

    public CreateComment(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public Comment Execute(string commentId, string description, string? taskId, string? employeeId)
    {
        var comment = new Comment
        {
            CommentId = commentId,
            Description = description,
            TaskId = taskId,
            EmployeeId = employeeId
        };

        _commentRepository.Add(comment);

        return comment;
    }
}
