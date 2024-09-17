using InformationSystems.Server.Data;
using InformationSystems.Server.DTO.Comment;
using InformationSystems.Server.Helper;
using InformationSystems.Server.Interfaces;
using InformationSystems.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystems.Server.Repository
{
    public class CommentRepository: ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return null;
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync(CommentQueryObject query)
        {
            var comments = _context.Comments.Include(c => c.AppUser).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.Symbol))
            {
                comments = comments.Where(c => c.Stock.Symbol == query.Symbol);
            }
            comments = query.isDescending == true ? comments.OrderByDescending(c => c.CreatedOn) : comments.OrderBy(c => c.CreatedOn);
            return await comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.Include(c => c.AppUser).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment request)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return null;
            }
            existingComment.Title = request.Title;
            existingComment.Content = request.Content;
            await _context.SaveChangesAsync();
            return existingComment;
        }
    }
}
