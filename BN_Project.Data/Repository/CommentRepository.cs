﻿using BN_Project.Core.Tools;
using BN_Project.Data.Context;
using BN_Project.Domain.Entities.Comment;
using BN_Project.Domain.IRepository;
using BN_Project.Domain.ViewModel.UserProfile.Comment;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BN_Project.Data.Repository
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly BNContext _context;
        public CommentRepository(BNContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<CommentRatingsViewModel>> GetAllRatingPoints(int productId)
        {
            return await _context.Comments.Where(n => n.IsConfirmed == true && n.User.IsDelete == false && n.ProductId == productId).Select(n => new CommentRatingsViewModel
            {
                BuildQuality = n.BuildQuality,
                DesignAndAppearance = n.DesignAndAppearance,
                EaseOfUse = n.EaseOfUse,
                FeaturesAndCapabilities = n.FeaturesAndCapabilities,
                Innovation = n.Innovation,
                ValueForMoneyComparedToTHePrice = n.ValueForMoneyComparedToTHePrice
            }).ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsWithRelations(Expression<Func<Comment, bool>> where = null)
        {
            IQueryable<Comment> query = _context.Comments;
            if (where != null)
                query = query.Where(where);

            return await query.Include(n => n.Strengths)
                .Include(n => n.WeakPoints)
                .Include(n => n.User)
                .Include(n => n.Impressions)
                .Include(n => n.Product)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<Comment> GetCommentWithRelations(Expression<Func<Comment, bool>> where)
        {
            return await _context.Comments.Where(where)
                .Include(n => n.Strengths)
                .Include(n => n.WeakPoints)
                .Include(n => n.User)
                .Include(n => n.Impressions)
                .AsSplitQuery()
                .SingleOrDefaultAsync();
        }
    }
}
