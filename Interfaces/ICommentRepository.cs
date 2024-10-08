﻿using InformationSystems.Server.DTO.Comment;
using InformationSystems.Server.Filter;
using InformationSystems.Server.Helper;
using InformationSystems.Server.Models;

namespace InformationSystems.Server.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(CommentQueryObject obj);
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> UpdateAsync(int id, Comment comment);
        Task<Comment> DeleteAsync(int id);
    }
}
