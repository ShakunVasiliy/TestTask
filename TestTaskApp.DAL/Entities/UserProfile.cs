using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTaskApp.DAL.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string Title { get; set; }
        public string Salutation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; }
        public string ImagePath { get; set; }

        public int? ManagerId { get; set; }
        public virtual UserProfile Manager { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }

        public UserProfile()
        {
            UserProfiles = new List<UserProfile>();
        }
    }
}
