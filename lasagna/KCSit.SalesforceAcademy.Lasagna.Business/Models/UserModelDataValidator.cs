using KCSit.SalesforceAcademy.Lasagna.Business.Models;
using KCSit.SalesforceAcademy.Lasagna.Business.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Models
{
    public static class UserModelDataValidator
    {

        public static UserServiceResultMessage CheckModelData(UserModel model)
        {

            // check if FirstName is valid
            var firstNameValidationReusltMessage = CheckFirstName(model);
            if (!firstNameValidationReusltMessage.Success)
            {
                return firstNameValidationReusltMessage;
            }

            // check if LastName is valid
            var lastNameValidationReusltMessage = CheckLastName(model);
            if (!lastNameValidationReusltMessage.Success)
            {
                return lastNameValidationReusltMessage;
            }

            // check if EmailAddress is valid
            var emailAddressValidationReusltMessage = CheckEmailAddress(model);
            if (!emailAddressValidationReusltMessage.Success)
            {
                return emailAddressValidationReusltMessage;
            }

            // check if password is valid (length, alpha-numeric, numbers, Capitals...)
            var passwordValidationReusltMessage = CheckPassword(model);
            if (!passwordValidationReusltMessage.Success)
            {
                return passwordValidationReusltMessage;
            }

            return new UserServiceResultMessage { Success = true, Message = "Model data is valid" };

        }


        private static UserServiceResultMessage CheckFirstName(UserModel model)
        {
            if (model.FirstName.Length < 2 || model.FirstName.Length > 15)
                return new UserServiceResultMessage { Success = false, Message = "First Name must have 2 to 15 characters" };

            var isFirstNameValid = new Regex(@"^([a-zA-Z]+)$");
            if (!isFirstNameValid.IsMatch(model.FirstName))
                return new UserServiceResultMessage { Success = false, Message = "First Name has invalid characters" };

            return new UserServiceResultMessage { Success = true, Message = "First Name is valid" };
        }

        private static UserServiceResultMessage CheckLastName(UserModel model)
        {
            if (model.LastName.Length < 2 || model.LastName.Length > 15)
                return new UserServiceResultMessage { Success = false, Message = "Last Name must have 2 to 15 characters" };

            var isLastNameValid = new Regex(@"^([a-zA-Z]+)$");
            if (!isLastNameValid.IsMatch(model.LastName))
                return new UserServiceResultMessage { Success = false, Message = "Last Name has invalid characters" };

            return new UserServiceResultMessage { Success = true, Message = "Last Name is valid" };
        }

        private static UserServiceResultMessage CheckEmailAddress(UserModel model)
        {
            var isEmailAddressValid = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!isEmailAddressValid.IsMatch(model.EmailAddress))
                return new UserServiceResultMessage { Success = false, Message = "Email Address is not valid" };

            return new UserServiceResultMessage { Success = true, Message = "Email Address is valid" };
        }


        private static UserServiceResultMessage CheckPassword(UserModel model)
        {

            var hasMinMaxChars = new Regex(@".{8,100}");
            if (!hasMinMaxChars.IsMatch(model.Password))
                return new UserServiceResultMessage { Success = false, Message = "Password must have at least 8 characters" };

            var hasUpperChar = new Regex(@"[A-Z]+");
            if (!hasUpperChar.IsMatch(model.Password))
                return new UserServiceResultMessage { Success = false, Message = "Password must have at least one capital letter" };

            var hasNumber = new Regex(@"[0-9]+");
            if (!hasNumber.IsMatch(model.Password))
                return new UserServiceResultMessage { Success = false, Message = "Password must have at least one numeric value" };

            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
            if (!hasSymbols.IsMatch(model.Password))
                return new UserServiceResultMessage { Success = false, Message = "Password must have at least one special character" };

            if (model.Password != model.ConfirmPassword)
                return new UserServiceResultMessage { Success = false, Message = "Password and Confirm Password do not match" };


            return new UserServiceResultMessage { Success = true, Message = "Password is valid" };
        }




    }
}
