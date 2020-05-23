using ContactInfo.API.Models;
using ContactInfo.Data;
using ContactInfo.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ContactInfo.API.Controllers
{
    [RoutePrefix("api/Contact")]
    [EnableCors("*", "*", "*")]
    public class ContactInfoController : ApiController
    {
        private readonly IContactInfoService _contactInfoService;
        public ContactInfoController(IContactInfoService contactInfoService)
        {
            _contactInfoService = contactInfoService;
        }

        /// <summary>
        /// To Display a List of All Contact Details
        /// </summary>
        /// <returns></returns>       
        [HttpGet]
        [Route("AllContactDetails")]
        public async Task<IHttpActionResult> GetDetails()
        {
            try
            {
                var contactDetails = _contactInfoService.GetAllContactInfo().ToList();
                if (contactDetails == null)
                {
                    return NotFound();
                }
                return await Task.FromResult(Json(contactDetails));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in fetching the contact details ", ex);
            }
        }

        /// <summary>
        /// Save Contact Details
        /// </summary>
        /// <returns></returns>   
        [HttpPost]
        [Route("ContactSubmission")]
        public async Task<IHttpActionResult> InsertContactInfo(tblContactInfo ContactInfo)
        {
            try
            {
                var result = _contactInfoService.SaveContactInfo(ContactInfo);
                return await Task.FromResult(Json(new { Result = result }));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Json("ERROR"));
            }
        }

        /// <summary>
        /// Update Contact Details
        /// </summary>
        /// <returns></returns>  
        [HttpPost]
        [Route("UpdateSubmission")]
        public async Task<IHttpActionResult> UpdateContactInfo(tblContactInfo contactInfo)
        {

            try
            {
                var result = _contactInfoService.UpdateContactInfo(contactInfo);
                return await Task.FromResult(Json(new { Result = result }));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Json("ERROR"));
            }
        }


        [HttpPost]
        [Route("RemoveContact")]
        public async Task<IHttpActionResult> RemoveContact(Deleteparam conID)
        {
            try
            {
                var result = _contactInfoService.RemoveContact(Convert.ToInt32(conID.DelContactID));
                return await Task.FromResult(Json(new { Result = result }));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Json("ERROR"));
            }
        }


    }
}
