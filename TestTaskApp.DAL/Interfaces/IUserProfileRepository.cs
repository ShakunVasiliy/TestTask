using System;
using System.Collections.Generic;

using TestTaskApp.DAL.Entities;


namespace TestTaskApp.DAL.Interfaces
{
    public interface IUserProfileRepository : IRepository<UserProfile>, ICRUDRepository<UserProfile>
    {
    }
}
