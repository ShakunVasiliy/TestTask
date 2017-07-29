using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using AutoMapper;

using TestTaskApp.BLL.DTO;
using TestTaskApp.BLL.Interfases;
using TestTaskApp.BLL.Util;
using TestTaskApp.WEB.Models;

namespace TestTaskApp.WEB.Controllers
{
    public class ImageController : Controller
    {
        private IUserProfileService userProfileService;

        public ImageController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        public ActionResult Edit(int id)
        {
            var userProfile = userProfileService.GetUserProfile(id);

            if (userProfile == null)
            {
                return HttpNotFound();
            }

            var imageEditViewModel = MappingUtil.MapToInstance<UserProfileDTO, ImageEditViewModel>(userProfile);

            return Request.IsAjaxRequest() ? (ActionResult)PartialView(imageEditViewModel)
                : View(imageEditViewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase file)
        {
            if (file != null)
            {
                var userProfile = userProfileService.GetUserProfile(id);

                if (userProfile != null)
                {
                    userProfile.ImagePath = "~/Images/" + id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath(userProfile.ImagePath));

                    userProfileService.UpdateUserProfile(userProfile);
                }
            }

            return RedirectToAction("Edit", "UserProfile", new { id });
        }

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            userProfileService.Dispose();
            base.Dispose(disposing);
        }

        #endregion Idisposable
    }
}