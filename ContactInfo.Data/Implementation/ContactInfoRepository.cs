using ContactInfo.Data.Interface;
using ContactInfo.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ContactInfo.Data.Implementation
{
    public class ContactInfoRepository : GenericRepository<tblContactInfo>, IContactInfoRepository
    {
        
        public ContactInfoRepository(IUnitOfWork context)
            : base(context)
        {
          
        } 
        
        public IEnumerable<tblContactInfo> GetAllContactInfo()
        {
           return GetAll().AsQueryable().OrderByDescending(x => x.ID);
        }
        public bool SaveContactInfo(tblContactInfo contdetails)
        {
            try
            {
                Add(contdetails);
                Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
       
        public bool UpdateContactInfo(tblContactInfo contdetails)
        {
            try
            {
                var contactinfo = (from getContdetails in (FindBy(x => x.ID == contdetails.ID)) select getContdetails).FirstOrDefault();
                contactinfo.Status = contdetails.Status;
                contactinfo.FirstName = contdetails.FirstName;
                contactinfo.LastName = contdetails.LastName;
                contactinfo.PhoneNumber = contdetails.PhoneNumber;
                contactinfo.Email = contdetails.Email;
                Update(contactinfo, contactinfo.ID);
                Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveContact(int ContactID)
        {
            try
            {
                var delContact = (from delContactDetails in (FindBy(x => x.ID == ContactID)) select delContactDetails).ToList();
                if(delContact.Count > 0)
                {
                    Delete(delContact.FirstOrDefault());
                    Save();                   
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}