using System;

using TestTaskApp.DAL.EF;
using TestTaskApp.DAL.Entities;
using TestTaskApp.DAL.Interfaces;

namespace TestTaskApp.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private TestTaskContext db;
        private UserProfileRepository userProfiles;
        private ManagerRepository managers;

        private bool isDisposed = false;

        public EFUnitOfWork(string connectionString)
        {
            this.db = new TestTaskContext(connectionString);
        }

        private void Dispose(bool isDisposing)
        {
            if (!this.isDisposed)
            {
                if (isDisposing)
                {
                    db.Dispose();
                }

                this.isDisposed = true;
            }
        }

        ~EFUnitOfWork()
        {
            Dispose(false);
        }

        #region IUnitOfWork

        public IUserProfileRepository UserProfiles
        {
            get
            {
                if (userProfiles == null)
                {
                    userProfiles = new UserProfileRepository(db);
                }

                return userProfiles;
            }
        }

        public IManagerRepository Managers
        {
            get
            {
                if (managers == null)
                {
                    managers = new ManagerRepository(db);
                }

                return managers;
            }
        }

        #region IDisposeble

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposeble

        #endregion IUnitOfWork
    }
}
