using ContactInfo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactInfo.Service.Interface
{
    public interface IContactInfoService
    {
        IEnumerable<tblContactInfo> GetAllContactInfo();
        bool SaveContactInfo(tblContactInfo contdetails);
        bool UpdateContactInfo(tblContactInfo contdetails);
        bool RemoveContact(int ContactID);
    }
}
