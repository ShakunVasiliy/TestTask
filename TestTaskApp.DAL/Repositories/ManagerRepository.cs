using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

using TestTaskApp.DAL.Interfaces;
using TestTaskApp.DAL.Entities;
using TestTaskApp.DAL.EF;

namespace TestTaskApp.DAL.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private TestTaskContext db;

        public ManagerRepository(TestTaskContext context)
        {
            db = context;
        }

        #region IManagerRepository

        public IEnumerable<Manager> GetAll()
        {
            var managers = (from u in db.UserProfiles.AsNoTracking()
                            select new Manager
                            {
                                Id = u.Id,
                                Name = u.Name
                            });

            return managers;
        }

        public IEnumerable<ManagerStatistics> GetStatistics()
        {
            var managersStatistics = (from u in db.UserProfiles.AsNoTracking()
                                      where u.UserProfiles.Count > 0
                                      select new ManagerStatistics
                                      {
                                          Name = u.Name,
                                          SubjectUserCount = u.UserProfiles.Count
                                      });

            return managersStatistics;

        }

        #endregion IMangerRepository
    }
}
