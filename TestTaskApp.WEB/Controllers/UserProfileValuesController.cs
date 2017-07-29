using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using TestTaskApp.BLL.DTO;
using TestTaskApp.BLL.Interfases;
using TestTaskApp.BLL.Infranstructure;
using TestTaskApp.BLL.Util;
using TestTaskApp.WEB.Models;

namespace TestTaskApp.WEB.Controllers
{
    public class UserProfileValuesController : ApiController
    {
        private IUserProfileService userProfileService;

        public UserProfileValuesController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var userProfileDtos = userProfileService.GetUserProfiles();
            var userProfiles = MappingUtil.MapToCollection<UserProfileDTO, UserProfileViewModel>(userProfileDtos);

            return Ok(userProfileDtos);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int? id)
        {
            if (id == null) return BadRequest();

            var userProfileDto = userProfileService.GetUserProfile(id.Value);

            if (userProfileDto == null) return BadRequest();

            return Ok(MappingUtil.MapToInstance<UserProfileDTO, UserProfileViewModel>(userProfileDto));
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]UserProfileWithoutImagePathViewModel userProfile)
        {
            var userProfileDto = MappingUtil.MapToInstance<UserProfileWithoutImagePathViewModel, UserProfileDTO>(userProfile);

            try
            {
                userProfileService.CreateUserProfile(userProfileDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]UserProfileWithoutImagePathViewModel userProfile)
        {
            var userProfileDto = MappingUtil.MapToInstance<UserProfileWithoutImagePathViewModel, UserProfileDTO>(userProfile);

            try
            {
                userProfileService.UpdateUserProfile(userProfileDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            userProfileService.DeleteUserProfile(id);

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            userProfileService.Dispose();
            base.Dispose(disposing);
        }
    }
}