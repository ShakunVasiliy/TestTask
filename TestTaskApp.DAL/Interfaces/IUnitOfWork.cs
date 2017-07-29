using System;

using TestTaskApp.DAL.Entities;

namespace TestTaskApp.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserProfileRepository UserProfiles { get; }
        IManagerRepository Managers { get; }
    }
}
