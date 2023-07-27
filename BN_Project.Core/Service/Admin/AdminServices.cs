﻿using BN_Project.Core.IService.Admin;
using BN_Project.Domain.IRepository;
using BN_Project.Domain.ViewModel.Admin;

namespace BN_Project.Core.Service.Admin
{
    public class AdminServices : IAdminServices
    {
        private readonly IUserRepository _userRepository;

        public AdminServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IReadOnlyList<UserListViewModel>> GetUsersForAdmin(int pageId)
        {
            List<UserListViewModel> result = new List<UserListViewModel>();

            var users = await _userRepository.GetAllUsers();

            if (users == null)
            {
                return result;
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            var lUsers = users.ToList().Skip(skip).Take(take).ToList();

            foreach (var user in lUsers)
            {
                result.Add(new UserListViewModel()
                {
                    PhoneNumber = (user.PhoneNumber != null) ? user.PhoneNumber : "---",
                    IsActive = user.IsActive,
                    Email = user.Email,
                    Name = (user.Name != null) ? user.Name : "---",
                    Id = user.Id
                });
            }

            return result.AsReadOnly();
        }
    }
}
