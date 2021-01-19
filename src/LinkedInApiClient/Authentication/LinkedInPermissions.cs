using System;
using System.Linq;

namespace LinkedInApiClient.Authentication
{
    /// <summary>
    /// Permissions	Description
    /// r_compliance	Required to retrieve activities for compliance monitoring and archiving.
    /// w_compliance	Required to manage and delete data for compliance.
    /// </summary>
    [Flags]
    public enum LinkedInPermissions
    {
        None = 0,

        /// <summary>
        /// Required to retrieve activities for compliance monitoring and archiving. (r_compliance)
        /// </summary>
        ReadCompliance = 0b0000001,

        /// <summary>
        /// Required to manage and delete data for compliance. (w_compliance)
        /// </summary>
        WriteCompliance = 0b0000010,

        /// <summary>
        /// Read - Basic profile (r_basicprofile)
        /// </summary>
        ReadBasicProfile = 0b0000100,

        /// <summary>
        /// Read - Email Address (r_emailaddress)
        /// </summary>
        ReadEmailAddress = 0b0001000,

        /// <summary>
        /// Read / Write - Company Admin (rw_company_admin)
        /// </summary>
        ReadWriteCompanyAdmin = 0b0010000,

        /// <summary>
        /// Write - Share (w_share)
        /// </summary>
        WriteShare = 0b0100000
    }
}

/// r_organization_social
///     Retrieve your organization's posts, comments, reactions, and other engagement data
/// r_1st_connections_size
///     Use your 1st-degree connections' data
/// r_ads_reporting
///     Retrieve reporting for your advertising accounts
/// rw_organization_admin
///     Manage your organizations' pages and retrieve reporting data
///     Manage an authenticated member’s company pages and retrieve reporting data.
/// r_basicprofile
///     Use your basic profile including your name, photo, headline, and public profile URL
/// r_ads
///     Retrieve your advertising accounts
/// rw_ads
///     Manage your advertising accounts
/// w_member_social
///     Create, modify, and delete posts, comments, and reactions on your behalf
/// w_organization_social
///     Create, modify, and delete posts, comments, and reactions on your organization's behalf
