using System;
using System.Linq;

namespace LinkedInApiClient.UseCases.Models
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/organizations/organization-access-control#organization-roles
    /// </summary>
    public enum OrganizationRoles
    {
        /// <summary>
        /// Access to administer an organizational entity. An administrator can post 
        /// updates, edit the organization's page, add other admins, view analytics, and 
        /// view notifications.
        /// </summary>
        ADMINISTRATOR,
        /// <summary>
        /// Access to read and create direct sponsored content for an organizational entity.
        /// </summary>
        DIRECT_SPONSORED_CONTENT_POSTER,
        /// <summary>
        /// Access to post to an organizational entity.
        /// </summary>
        RECRUITING_POSTER,
        /// <summary>
        /// Access to view and manage landing pages for the company, as well as
        /// create new landing pages or edit existing ones.
        /// </summary>
        LEAD_CAPTURE_ADMINISTRATOR,
        /// <summary>
        /// Access to retrieve leads that belong to a specific account which is associated
        /// with a company page.
        /// </summary>
        LEAD_GEN_FORMS_MANAGER
    }
}
