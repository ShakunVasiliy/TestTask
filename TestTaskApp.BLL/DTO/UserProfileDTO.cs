using System;

namespace TestTaskApp.BLL.DTO
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Salutation { get; set; }
        public int? ManagerId { get; set; }
        public string ManagerName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; }
        public string ImagePath { get; set; }
    }
}
