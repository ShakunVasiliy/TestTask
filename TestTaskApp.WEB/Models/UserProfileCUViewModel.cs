using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TestTaskApp.WEB.Models
{
    public class UserProfileCUViewModel
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

        public SelectList Managers { get; set; }
        public SelectList Salutations { get; set; }

        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public FormMethod FormMethod { get; set; }
        public string SubmitValue { get; set; }
    }
}