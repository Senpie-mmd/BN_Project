﻿using BN_Project.Domain.ViewModel.UserProfile;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using BN_Project.Core.IService.Account;
using BN_Project.Core.Response.DataResponse;
using BN_Project.Core.Tools;

namespace BN_Project.Web.Controllers.UserProfile
{
    public class UserChangePassController : Controller
    {
        private readonly IAccountServices _accountService;
        public UserChangePassController(IAccountServices AccountService)
        {
            _accountService = AccountService;
        }

        private DataResponse<UserInformationViewModel> GetCurrentUser()
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault().Value);
            var user = _accountService.GetUserInformationById(UserId).Result;
            return user;
        }

        public UserLoginInformationViewModel UserLoginInfoVM { get; set; }

        public IActionResult Index()
        {
            return View("~/Views/UserChangePass/ChangePassword.cshtml");
        }
    }
}