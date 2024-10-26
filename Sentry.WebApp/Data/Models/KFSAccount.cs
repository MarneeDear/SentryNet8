using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("KFSAccounts", Schema = "MDS")]
    public class KFSAccount
    {
        [Column("Id")]
        public string Id { get; set; }
        //public string Text { get; set; }

        [Column("KFSAccountCode")]
        public string KFSAccountCode { get; set; }
    }
}
