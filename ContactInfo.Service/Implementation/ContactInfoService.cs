using ContactInfo.Data;
using ContactInfo.Data.Interface;
using ContactInfo.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactInfo.Service.Implementation
{
    public class ContactInfoService : IContactInfoService
    {
        protected IContactInfoRepository _repository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        public ContactInfoService(IContactInfoRepository repo)
        {
            _repository = repo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<tblContactInfo> GetAllContactInfo()
        {
            return _repository.GetAllContactInfo();

        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool SaveContactInfo(tblContactInfo contdetails)
        {
            return _repository.SaveContactInfo(contdetails);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool UpdateContactInfo(tblContactInfo contdetails)
        {
            return _repository.UpdateContactInfo(contdetails);
        }

        public bool RemoveContact(int ContactID)
    {
            return _repository.RemoveContact(ContactID);
        }
    }
}