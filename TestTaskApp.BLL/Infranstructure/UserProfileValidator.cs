using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using TestTaskApp.BLL.Interfases;
using TestTaskApp.BLL.Infranstructure;
using TestTaskApp.DAL.Entities;
using TestTaskApp.DAL.Interfaces;

namespace TestTaskApp.BLL.Infranstructure
{
    public class UserProfileValidator : IValidator<UserProfile>
    {
        private IUnitOfWork dataset;
        private UserProfile userProfile;
        private string emailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" 
            + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
        
        public UserProfileValidator(IUnitOfWork unitOfWork)
        {
            dataset = unitOfWork;
        }

        private void ValidateName()
        {
            if (string.IsNullOrEmpty(userProfile.Name))
            {
                throw new ValidationException("The name is empty.", "Name");
            }
        }

        private void ValidateEmail()
        {
            if (string.IsNullOrEmpty(userProfile.Email))
            {
                throw new ValidationException("The email is empty.", "Email");
            }

            if (!Regex.IsMatch(userProfile.Email, emailPattern, RegexOptions.IgnoreCase))
            {
                throw new ValidationException("Wrong the e-mail address.", "Email");
            }
        }

        private void ValidateManagerId()
        {
            if (userProfile.ManagerId == null) return;

            if (dataset.UserProfiles.Read(userProfile.ManagerId.Value) == null)
            {
                throw new ValidationException("The manager is not found.", "ManagerId");
            }
        }

        public void ValidateDateOfBirth()
        {
            if (userProfile.DateOfBirth == null)
            {
                throw new ValidationException("The date of birth is not setted.", "DateOfBirth");
            }

            if (userProfile.DateOfBirth < new DateTime(1900, 1, 1))
            {
                throw new ValidationException("The date of birth have to be more than 1900.01.01", "DateOfBirth");
            }
        }

        #region IValidator<T>
            
        public void Validate(UserProfile userProfile)
        {
            this.userProfile = userProfile;

            ValidateName();
            ValidateEmail();
            ValidateManagerId();
            ValidateDateOfBirth();
        }

        #endregion IValidator<T>
    }
}
