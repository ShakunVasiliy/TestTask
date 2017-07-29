using System;
using System.Collections.Generic;

using TestTaskApp.BLL.DTO;
using TestTaskApp.BLL.Infranstructure;
using TestTaskApp.DAL.Entities;

namespace TestTaskApp.BLL.Interfases
{
    public interface IUserProfileService : IServiceWithSort<UserProfileDTO>, IDisposable
    {
        string DefaultImagePath { get; set; }

        IEnumerable<UserProfileDTO> GetUserProfiles();
        IEnumerable<UserProfileDTO> GetUserProfiles(SortParameter sortParameter);
        UserProfileDTO GetUserProfile(int id);
        void CreateUserProfile(UserProfileDTO userProfileDto);
        void UpdateUserProfile(UserProfileDTO userProfileDto);
        void DeleteUserProfile(int id);
    }
}
