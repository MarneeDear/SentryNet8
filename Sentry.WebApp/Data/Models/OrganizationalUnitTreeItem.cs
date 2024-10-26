using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("OrganizationalUnitsTree", Schema = "MDS")]
    public class OrganizationalUnitTreeItem
    {
        [NotMapped]
        public A_attr a_attr { get; set; }
        public int id { get; set; }
        public string code { get; set; }
        public string orgUnitCode { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        
        public string name { get; set; }
        public string nameSource { get; set; }
        public DateTime? nameDate { get; set; }              

        public string orgUnitType { get; set; }
        public string orgUnitTypeSource { get; set; }
        public DateTime? orgUnitTypeDate { get; set; }

        public string org { get; set; }
        public string orgSource { get; set; }
        public string orgDate { get; set; }

        public string parentOrgUnit { get; set; }
        public string parentOrgUnitType { get; set; }
        public string parentOrgUnitCode { get; set; }
        public string parentOrgUnitSource { get; set; }
        public DateTime? parentOrgUnitDate { get; set; }

        public string avatar { get; set; }

        public string Dean { get; set; }

        [NotMapped]
        public IEnumerable<OrganizationalUnitTreeItem> children { get; set; }
    }
}
