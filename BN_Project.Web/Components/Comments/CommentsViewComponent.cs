﻿using BN_Project.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BN_Project.Web.Components.Comments
{
    public class CommentsViewComponent : ViewComponent
    {
        private readonly ICommentServices _commentServices;
        public CommentsViewComponent(ICommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var comments = await _commentServices.GetAllCommentsForUsers();

            return View("Comments", comments);
        }
    }
}
