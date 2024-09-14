using InformationSystems.Server.Models;

namespace InformationSystems.Server.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment> DeleteAsync(int id);
    }
}
