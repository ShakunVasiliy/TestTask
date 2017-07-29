using System;
using AutoMapper;

using TestTaskApp.DAL.Entities;
using TestTaskApp.BLL.DTO;

namespace TestTaskApp.BLL.Util
{
    public static class MapInitializer
    {
        public static void UserProfileToUserProfileDTO()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<UserProfile, UserProfileDTO>()
                .ForMember("ManagerName", opt => opt.MapFrom(src => src.Manager != null ? src.Manager.Name : "")));
        }
    }
}
