using Business_Logic.Comments;

namespace Application.Comments;

/// <summary>
/// Reads comments, either all of them or by id.
/// </summary>
public class GetComment
{
    private readonly ICommentRepository _commentRepository;

    public GetComment(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public List<Comment> GetAll()
    {
        return _commentRepository.GetAll();
    }

    public Comment? GetById(string commentId)
    {
        return _commentRepository.GetById(commentId);
    }
}
