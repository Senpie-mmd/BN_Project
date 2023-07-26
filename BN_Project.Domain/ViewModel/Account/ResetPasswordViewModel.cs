﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BN_Project.Domain.ViewModel.Account
{
    public class ResetPasswordViewModel
    {
        public string Token { get; set; }

        [DisplayName("رمز عبور")]
        [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
        [MinLength(8, ErrorMessage = "حداقل حروف رمز 8 رقم میباشد")]
        public string Password { get; set; }

        [DisplayName("تکرار رمز عبور")]
        [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
        [MinLength(8, ErrorMessage = "حداقل حروف رمز 8 رقم میباشد")]
        [Compare(nameof(Password), ErrorMessage = "{0} با {1} یکسان نمیباشند")]
        public string ConfirmPassword { get; set; }
    }
}
