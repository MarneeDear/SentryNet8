using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
    public class DesignationPossibleMatch
    {
        [Key]
        public string MasterId { get; set; }
        public int MatchConfidence { get; set; }

        #region Match
        public string DesignationId { get; set; }

        public string DesignationName { get; set; }

        public string KFSAccount { get; set; }

        public string VSECategoryName { get; set; }

        #endregion

    }
}
