﻿using BN_Project.Domain.Entities;

namespace BN_Project.Domain.IRepository
{
    public interface IAccountRepository
    {
        public Task<UserEntity> RegisterUsere(UserEntity register);

        public Task<UserEntity> GetUserByEmail(string email);

        public Task<UserEntity> GetUserByToken(string token);

        public Task<UserEntity> GetUserById(int id);

        public void UpdateUser(UserEntity user);
        public void DeleteUser(int Id);
        public Task SaveChanges();
    }
}
