﻿using BN_Project.Core.Response;
using BN_Project.Core.Response.DataResponse;
using BN_Project.Domain.Entities;
using BN_Project.Domain.ViewModel.Admin;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BN_Project.Core.IService.Admin
{
    public interface IAdminServices
    {
        #region Users

        public Task<DataResponse<IReadOnlyList<UserListViewModel>>> GetUsersForAdmin(int pageId);

        public Task<BaseResponse> AddUserFromAdmin(AddUserViewModel addUser);
        public Task<EditUserViewModel> GetUserById(int Id);
        public Task<bool> RemoveUserById(int Id);
        public Task<BaseResponse> EditUsers(EditUserViewModel user);

        #endregion

        #region Product

        public Task<DataResponse<IReadOnlyList<ProductListViewModel>>> GetProducts(int pageId = 1);

        public Task<SelectList> GetParentCategories();

        public Task<SelectList> GetSubCategories(int parentId);

        public Task<BaseResponse> AddProduct(AddProductViewModel addProduct);

        #endregion
        public Task<List<CategoriesViewModel>> GetAllCategories();

        public Task<bool> AddCategory(AddCategoriesViewModel category);
        public Task<bool> RemoveCatagory(int Id);
        public Task<EditCategoryViewModel> GetCategoryById(int Id);
        public Task<bool> EditCategory(EditCategoryViewModel category);
    }
}
