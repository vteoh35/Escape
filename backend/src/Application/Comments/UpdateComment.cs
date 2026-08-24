using Business_Logic.Comments;

namespace Application.Comments;

public class UpdateComment
{
    private readonly ICommentRepository _commentRepository;

    public UpdateComment(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public Comment? Execute(string commentId, string description)
    {
        var comment = _commentRepository.GetById(commentId);

        if (comment == null)
        {
            return null;
        }

        comment.Description = description;

        _commentRepository.Update(comment);

        return comment;
    }
}
