using System;
using System.ComponentModel.DataAnnotations;
namespace Sentry.WebApp.Data.Models
{
    public class DegreePossibleMatch
    {
        [Key]
        public string MasterId { get; set; }
        public int MatchConfidence { get; set; }

        #region Match

        public string DegreeTerm { get; set; }

        #endregion
    }
}
