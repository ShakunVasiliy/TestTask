using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using TestTaskApp.DAL.Entities;
using TestTaskApp.DAL.Interfaces;
using TestTaskApp.BLL.DTO;
using TestTaskApp.BLL.Interfases;
using TestTaskApp.BLL.Infranstructure;
using TestTaskApp.BLL.Util;

namespace TestTaskApp.BLL.Services
{
    public class UserProfileService : IUserProfileService
    {
        private IUnitOfWork dataset;
        private IValidator<UserProfile> userProfileValidator;
        private ISorter<UserProfile> userProfileSorter;

        public UserProfileService(IUnitOfWork unitOfWork, IValidator<UserProfile> userProfileValidator,
            ISorter<UserProfile> userProfileSorter, string defaultImagePath)
        {
            this.dataset = unitOfWork;
            this.userProfileValidator = userProfileValidator;
            this.userProfileSorter = userProfileSorter;
            this.DefaultImagePath = defaultImagePath;
        }

        #region IUserProfileService

        public string DefaultImagePath { get; set; }

        public IEnumerable<UserProfileDTO> GetUserProfiles()
        {
            return GetWithSortBy(new SortParameter());
        }

        public IEnumerable<UserProfileDTO> GetUserProfiles(SortParameter sortParameter)
        {
            return GetWithSortBy(sortParameter);
        }

        public UserProfileDTO GetUserProfile(int id)
        {
            var userProfile = dataset.UserProfiles.Read(id);

            MapInitializer.UserProfileToUserProfileDTO();

            return Mapper.Map<UserProfile, UserProfileDTO>(userProfile);
        }

        public void CreateUserProfile(UserProfileDTO userProfileDto)
        {
            var userProfile = MappingUtil.MapToInstance<UserProfileDTO, UserProfile>(userProfileDto);
            
            userProfileValidator.Validate(userProfile);

            userProfile.ImagePath = string.IsNullOrEmpty(userProfile.ImagePath) ? DefaultImagePath :
                userProfile.ImagePath;

            dataset.UserProfiles.Create(userProfile);

            userProfileDto.Id = userProfile.Id;
        }

        public void UpdateUserProfile(UserProfileDTO userProfileDto)
        {
            var userProfile = MappingUtil.MapToInstance<UserProfileDTO, UserProfile>(userProfileDto);

            userProfile.ImagePath = string.IsNullOrEmpty(userProfile.ImagePath) ? DefaultImagePath :
                userProfile.ImagePath;

            userProfileValidator.Validate(userProfile);
            
            dataset.UserProfiles.Update(userProfile);
        }

        public void DeleteUserProfile(int id)
        {
            dataset.UserProfiles.Delete(id);
        }

        #region IDisposeble

        public void Dispose()
        {
            dataset.Dispose();
        }

        #endregion IDisposeble

        #endregion IUserProfileService

        #region IServiceWithSort

        public IEnumerable<UserProfileDTO> GetWithSortBy(SortParameter sortParameter)
        {
            var userProfiles = dataset.UserProfiles.GetAll();
            userProfiles = userProfileSorter.Sort(userProfiles, sortParameter);

            MapInitializer.UserProfileToUserProfileDTO();

            return Mapper.Map<IEnumerable<UserProfile>, List<UserProfileDTO>>(userProfiles);
        }

        #endregion IServiceWithSort
    }
}
