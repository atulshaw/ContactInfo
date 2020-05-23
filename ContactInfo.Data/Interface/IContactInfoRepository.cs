using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactInfo.Data.Interface
{
    public interface IContactInfoRepository
    {
        IEnumerable<tblContactInfo> GetAllContactInfo();
        bool SaveContactInfo(tblContactInfo contdetails);
        bool UpdateContactInfo(tblContactInfo contdetails);
        bool RemoveContact(int ContactID);        
    }
}
