using System.Collections.Generic;

namespace Sentry.Domain.AccountsPayable.Entities
{
    public class Constants
    {
        public const string ETForm = "ET";
        public const string STForm = "ST";
        public const string EMForm = "EM";

        public const string FinancialServicesStewardRole = "Financial Services Steward Role";
        public const string SignatureAuthorityRole = "Signature Authority";
        public const string DesigneeRole = "Designee";
        public const string APReviewerRole = "AP Review";
        public const string GeneralCounselRole = "General Counsel";
        public const string AssociateVPRole = "Associate Vice President";
        public const string VPRole = "Vice President";
        public const string CFORole = "Chief Financial Officer";
        public const string ScholarshipRole = "Scholarship";
        public const string APManagerRole = "AP Manager";
        //public const string GiftProcessingRole = "Gift Processing";
        public const string ARStaffRole = "AR Staff";
        public const string ARSupervisorReviewerRole = "AR Supervisor Reviewer";
        public const string ARGUStaffRole = "AR GU Staff";
        public const string AdminRole = "Admin";

        //Funds transfer roles
        public const string FTUnrestrictedCampusRole = "FT Unrestricted (Campus)"; //Campus/UAFDN side approver
        public const string FTReviewerRole = "FT Reviewer";
        public const string FTApproverRole = "FT Approver";
        //public const string RestrictedFTApproverRole = "Restricted FT Approver";
        //public const string UnrestrictedFTApproverRole = "Unrestricted FT Approver";
        //public const string GiftFTApproverRole = "Gift FT Approver";
        //public const string EndowmentFTApproverRole = "Endowment FT Approver";
        public const string FTGeneralCounselApproverRole = "FT General Counsel Approver";

        public const int SignatureAuthorityRoleId = 1;
        public const int DesigneeRoleId = 2;
        public const int APReviewerRoleId = 3;
        public const int GeneralCounselRoleId = 4;
        public const int AssistantVPRoleId = 5;
        public const int VPRoleId = 6;
        public const int CFORoleId = 7;
        public const int ScholarshipRoleId = 9;
        public const int APManagerRoleId = 10;
        public const int AdminRoleId = 9999;

        //Funds Transfer roles ids
        public const int FTUnrestrictedCampusRoleId = 12;
        public const int FTReviewerRoleId = 13;
        public const int FTApproverRoleId = 14;
        //public const int RestrictedFTApproverRoleId = 14;
        //public const int UnrestrictedFTApproverRoleId = 15;
        //public const int GiftFTApproverRoleId = 16;
        //public const int EndowmentFTApproverRoleId = 17;
        public const int FTGeneralCounselApproverRoleId = 15;

        //ROUTING TYPES/BUCKETS
        public const int RestrictedBucketId = 1;
        public const int UnrestrictedBucketId = 2;
        public const int EndowmentBucketId = 3;
        public const int GiftBucketId = 4;
        public const int UnroutedBucketId = 5;
        public const int GeneralCounselBucketId = 6;

        public const string RestrictedBucket = "Restricted";
        public const string UnrestrictedBucket = "Unrestricted";
        public const string EndowmentBucket = "Endowment";
        public const string GiftBucket = "Gift";
        public const string UnroutedBucket = "Unrouted";
        public const string GeneralCounselBucket = "General Counsel";

        public static IDictionary<int, string> APApproverRoles
        {
            get
            {
                /*
                 * Id	Description	GroupEmail
                1	Signature Authority	NULL
                2	Designee	NULL
                3	AP Review	NULL
                4	General Counsel	OGC@uafoundation.org
                5	Assistant Vice President	AVPFinancialServices@uafoundation.org
                6	Vice President	VPFinancialServices@uafoundation.org  
                7	Chief Financial Officer	CFO@uafoundation.org
                8	Provost	NULL
                9	Scholarship	NULL
                10	AP Manager	NULL
                11	Alternate Escalated Approver	NULL
                 */
                return new Dictionary<int, string>()
                {
                    { SignatureAuthorityRoleId, SignatureAuthorityRole},
                    { DesigneeRoleId, DesigneeRole },
                    { APReviewerRoleId, APReviewerRole },
                    { GeneralCounselRoleId, GeneralCounselRole },
                    { AssistantVPRoleId, AssociateVPRole },
                    { VPRoleId, VPRole },
                    { CFORoleId, CFORole },
                    { ScholarshipRoleId, ScholarshipRole },
                    { APManagerRoleId, APManagerRole },
                    { AdminRoleId, AdminRole }
                };
            }
        }

        public static IDictionary<string, int> APApproverRoleIds
        {
            get
            {
                /*
                 * Id	Description	GroupEmail
                1	Signature Authority	NULL
                2	Designee	NULL
                3	AP Review	NULL
                4	General Counsel	OGC@uafoundation.org
                5	Assistant Vice President	AVPFinancialServices@uafoundation.org
                6	Vice President	VPFinancialServices@uafoundation.org  
                7	Chief Financial Officer	CFO@uafoundation.org
                8	Provost	NULL
                9	Scholarship	NULL
                10	AP Manager	NULL
                11	Alternate Escalated Approver	NULL
                 */
                return new Dictionary<string, int>()
                {
                    { SignatureAuthorityRole, SignatureAuthorityRoleId},
                    { DesigneeRole, DesigneeRoleId },
                    { APReviewerRole, APReviewerRoleId },
                    { GeneralCounselRole, GeneralCounselRoleId },
                    { AssociateVPRole, AssistantVPRoleId },
                    { VPRole, VPRoleId },
                    { CFORole, CFORoleId },
                    { ScholarshipRole, ScholarshipRoleId },
                    { APManagerRole, APManagerRoleId },
                    { AdminRole, AdminRoleId }
                };
            }
        }

        public static IDictionary<int, string> FTApproverRoles
        {
            get
            {
                /*
                 * Id	Description	GroupEmail
                1	Signature Authority	NULL
                2	Designee	NULL
                12	Funds Transfer Financial Services	(null)
                13	FT Reviewer	(null)
                14	Restricted FT Approver	(null)
                15	FT General Counsel Approver	(null)
                 */
                return new Dictionary<int, string>()
                {
                    { SignatureAuthorityRoleId, SignatureAuthorityRole},
                    { DesigneeRoleId, DesigneeRole },
                    { FTUnrestrictedCampusRoleId, FTUnrestrictedCampusRole },
                    { FTReviewerRoleId, FTReviewerRole },
                    { FTApproverRoleId, FTApproverRole },
                    //{ RestrictedFTApproverRoleId, RestrictedFTApproverRole },
                    //{ UnrestrictedFTApproverRoleId, UnrestrictedFTApproverRole },
                    //{ GiftFTApproverRoleId, GiftFTApproverRole },
                    //{ EndowmentFTApproverRoleId, EndowmentFTApproverRole },
                    { FTGeneralCounselApproverRoleId, FTGeneralCounselApproverRole },
                    { AdminRoleId, AdminRole }
                };
            }
        }

        public static IDictionary<string, int> FTApproverRoleIds
        {
            get
            {
                /*
                 * Id	Description	GroupEmail
                1	Signature Authority	NULL
                2	Designee	NULL
                12	Funds Transfer Financial Services	(null)
                13	FT Reviewer	(null)
                14	FT Approver	(null)
                15	FT General Counsel Approver	(null)
                 */
                return new Dictionary<string, int>()
                {
                    { SignatureAuthorityRole, SignatureAuthorityRoleId},
                    { DesigneeRole, DesigneeRoleId },
                    { FTUnrestrictedCampusRole, FTUnrestrictedCampusRoleId },
                    { FTReviewerRole, FTReviewerRoleId },
                    { FTApproverRole, FTApproverRoleId },
                    //{ RestrictedFTApproverRole, RestrictedFTApproverRoleId },
                    //{ UnrestrictedFTApproverRole, UnrestrictedFTApproverRoleId },
                    //{ GiftFTApproverRole, GiftFTApproverRoleId },
                    //{ EndowmentFTApproverRole, EndowmentFTApproverRoleId },
                    { FTGeneralCounselApproverRole, FTGeneralCounselApproverRoleId },
                    { AdminRole, AdminRoleId }
                };
            }
        }


    }
}
