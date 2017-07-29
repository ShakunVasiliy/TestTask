using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;

using TestTaskApp.BLL.DTO;
using TestTaskApp.BLL.Interfases;
using TestTaskApp.BLL.Infranstructure;
using TestTaskApp.BLL.Util;
using TestTaskApp.WEB.Models;
using TestTaskApp.WEB.Util;

namespace TestTaskApp.WEB.Controllers
{
    public class UserProfileController : Controller
    {
        private const int CountValuesOnPage = 3;

        private IUserProfileService userProfileService;
        private IManagerService managerService;
        private ISalutationService salutationService;

        public UserProfileController(IUserProfileService userProfileService, IManagerService managerService,
            ISalutationService salutationService)
        {
            this.userProfileService = userProfileService;
            this.managerService = managerService;
            this.salutationService = salutationService;
        }

        // GET: UserProfile
        public ActionResult Index(int? page, SortParameter sortParameter)
        {
            page = page ?? 1;

            IEnumerable<UserProfileDTO> userProfileDtos = userProfileService.GetUserProfiles(sortParameter);
            var viewModel = new UserProfileListViewModel();

            viewModel.currentSortParameter = sortParameter;
            viewModel.SortParameters = SortParameter.CreateSortParameters(sortParameter, 
                new string[] { "Name", "Email", "Title", "ManagerName" });
            
            viewModel.List = MappingUtil.MapToCollection<UserProfileDTO, UserProfileForListViewModel>(userProfileDtos)
                .ToPagedList(page.Value, CountValuesOnPage);


            return Request.IsAjaxRequest() ? (ActionResult)PartialView(viewModel) 
                : View(viewModel);
        }

        // GET: UserProfile/Create
        [HttpGet]
        public ActionResult Create()
        {
            var userProfile = new UserProfileCUViewModel()
            {
                DateOfBirth = DateTime.Now
            };

            SetModelCUFormProperties(userProfile, "Create", "UserProfile", "Create");
            
            return Request.IsAjaxRequest() ? (ActionResult)PartialView(userProfile)
                : View(userProfile);
        }

        // POST: UserProfile/Create
        [HttpPost]
        public ActionResult Create(UserProfileViewModel userProfile)
        {
            UserProfileDTO userProfileDto = MappingUtil.MapToInstance<UserProfileViewModel, UserProfileDTO>(userProfile);

            try
            {
                userProfileService.CreateUserProfile(userProfileDto);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                
                var userProfileCU = MappingUtil.MapToInstance<UserProfileViewModel, UserProfileCUViewModel>(userProfile);

                SetModelCUFormProperties(userProfileCU, "Create", "UserProfile", "Create");

                return Request.IsAjaxRequest() ? (ActionResult)PartialView(userProfileCU)
                    : View(userProfileCU);
            }

            return RedirectToAction("Index");
        }

        // GET: UserProfile/Edit/5
        public ActionResult Edit(int id)
        {
            var userProfileDto = userProfileService.GetUserProfile(id);

            if (userProfileDto == null)
            {
                return HttpNotFound();
            }

            var userProfileCU = MappingUtil.MapToInstance<UserProfileDTO, UserProfileCUViewModel>(userProfileDto);

            SetModelCUFormProperties(userProfileCU, "Edit", "UserProfile", "Edit");

            return Request.IsAjaxRequest() ? (ActionResult)PartialView(userProfileCU)
                : View(userProfileCU);
        }

        // POST: UserProfile/Edit/5
        [HttpPost]
        public ActionResult Edit(UserProfileViewModel userProfile)
        {
            var oldUserProfileDto = userProfileService.GetUserProfile(userProfile.Id);

            if (oldUserProfileDto == null)
            {
                return RedirectToAction("Index");
            }

            var userProfileDto = MappingUtil.MapToInstance<UserProfileViewModel, UserProfileDTO>(userProfile);
            userProfileDto.ImagePath = oldUserProfileDto.ImagePath;

            try
            {
                userProfileService.UpdateUserProfile(userProfileDto);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                
                var userProfileCU = MappingUtil.MapToInstance<UserProfileViewModel, UserProfileCUViewModel>(userProfile);

                SetModelCUFormProperties(userProfileCU, "Edit", "UserProfile", "Edit");

                return Request.IsAjaxRequest() ? (ActionResult)PartialView(userProfileCU)
                    : View(userProfileCU);
            }

            return RedirectToAction("Index");
        }

        // GET: UserProfile/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var userProfileDto = userProfileService.GetUserProfile(id);

            if (userProfileDto == null)
            {
                return HttpNotFound();
            }

            var userProfile = MappingUtil.MapToInstance<UserProfileDTO, UserProfileDeleteViewModel>(userProfileDto);

            return Request.IsAjaxRequest() ? (ActionResult)PartialView(userProfile)
                : View(userProfile);
        }

        // POST: UserProfile/Delete/5
        [HttpPost]
        public ActionResult Delete(UserProfileDeleteViewModel userProfile)
        {
            try
            {
                userProfileService.DeleteUserProfile(userProfile.Id);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);

                return RedirectToAction("Delete", new { id = userProfile.Id });
            }

            return RedirectToAction("Index");
        }

        private void SetModelCUFormProperties(UserProfileCUViewModel model, string actionName, 
            string controllerName, string submitValue)
        {
            var managers = managerService.GetManagers().Where(m => m.Id != model.Id);
            var salutations = salutationService.GetSalutations();

            // делаю так потому, что нужна возможность ни чего не выбирать из списка.
            managers = managers.AddToFirstPosition(new ManagerDTO());
            salutations = salutations.AddToFirstPosition(string.Empty);

            model.Salutations = new SelectList(salutations);
            model.Managers = new SelectList(managers, "Id", "Name");

            model.ActionName = actionName;
            model.ControllerName = controllerName;
            model.FormMethod = FormMethod.Post;
            model.SubmitValue = submitValue;
        }        

        protected override void Dispose(bool disposing)
        {
            userProfileService.Dispose();
            managerService.Dispose();
            salutationService.Dispose();
            base.Dispose(disposing);
        }
    }
}
