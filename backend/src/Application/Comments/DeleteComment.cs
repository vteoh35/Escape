namespace Application.Comments;

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
