using System;
namespace BusinessObjects
{
    interface IGardensOrganizations
    {
        bool Active { get; set; }
        string ContactPhone { get; set; }
        int OrganizationID { get; set; }
        int OrganizationLeader { get; set; }
        string OrganizationName { get; set; }
    }
}
