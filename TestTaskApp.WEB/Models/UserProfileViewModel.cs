using System;
using System.ComponentModel.DataAnnotations;

namespace TestTaskApp.WEB.Models
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Salutation { get; set; }
        public int? ManagerId { get; set; }
        public string Bio { get; set; }
        public string ImagePath { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Bate of birth")]
        public DateTime DateOfBirth { get; set; }
    }
}