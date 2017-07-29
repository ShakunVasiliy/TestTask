using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

using TestTaskApp.DAL.Interfaces;
using TestTaskApp.DAL.Entities;
using TestTaskApp.DAL.EF;

namespace TestTaskApp.DAL.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private TestTaskContext db;

        public UserProfileRepository(TestTaskContext context)
        {
            this.db = context;
        }

        #region IUserProfileRepository

        public IEnumerable<UserProfile> GetAll()
        {
            return db.UserProfiles.AsNoTracking().Include("Manager");
        }

        public UserProfile Read(int id)
        {
            return db.UserProfiles.AsNoTracking().Where(up => up.Id == id).FirstOrDefault();
        }

        public void Create(UserProfile userProfile)
        {
            db.UserProfiles.Add(userProfile);
            db.SaveChanges();
        }

        public void Update(UserProfile userProfile)
        {
            if (Read(userProfile.Id) == null) return;

            db.Entry(userProfile).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {

            var userProfile = db.UserProfiles.Find(id);
           
            if (userProfile != null)
            {
                userProfile.UserProfiles.Clear();
                db.UserProfiles.Remove(userProfile);
                db.SaveChanges();
            }
        }

        #endregion IUserProfileRepository
    }
}
