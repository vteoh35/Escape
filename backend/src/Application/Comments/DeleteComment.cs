namespace Application.Comments;

/// <summary>
/// Deletes a comment. Returns false if the id doesn't exist.
/// </summary>
public class DeleteComment
{
    private readonly ICommentRepository _commentRepository;

    public DeleteComment(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public bool Execute(string commentId)
    {
        var comment = _commentRepository.GetById(commentId);

        if (comment == null)
        {
            return false;
        }

        _commentRepository.Delete(comment);

        return true;
    }
}
