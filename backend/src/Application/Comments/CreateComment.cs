using Business_Logic.Comments;

namespace Application.Comments;

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
