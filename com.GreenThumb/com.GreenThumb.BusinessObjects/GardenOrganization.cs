using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Dat Tran
namespace BusinessObjects
{
    public class GardensOrganizations : BusinessObjects.IGardensOrganizations
    {
        public int OrganizationID  {get;set;}
        public string OrganizationName  {get;set;}
        public int OrganizationLeader  {get;set;}
        public string ContactPhone  {get;set;}
        public bool Active {get;set;}
        
        
        public GardensOrganizations(int organizationID,
                    string organizationName,
                    int organizationLeader,
                    string contactPhone,
                    bool active) 
        {
            OrganizationID = organizationID;
            OrganizationName = organizationName;
            OrganizationLeader = organizationLeader;
            ContactPhone = contactPhone;
            Active = active;
        }
    }
}
