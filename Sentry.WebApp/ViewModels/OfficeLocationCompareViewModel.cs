using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Sentry.WebApp.Data;
using Sentry.WebApp.Data.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;

namespace Sentry.WebApp.ViewModels
{
    public class OfficeLocationCompareViewModel
    {
        public DateTime? IntegrationDate { get; set; }

        public long? Id { get; set; }
        public int IntegrationId { get; set; }
        public int SystemId { get; set; }
        public int? MasterId { get; set; }

        public string System { get; set; }

        public string SourceRecordId { get; set; }



        #region Integration Record

        public string Name { get; set; }

        public string BuildingCode { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        #endregion


        #region Compare Record

        public string SourceRecordId_Compare { get; set; }

        public string Name_Compare { get; set; }

        public string BuildingCode_Compare { get; set; }

        public string Address1_Compare { get; set; }

        public string Address2_Compare { get; set; }

        public string City_Compare { get; set; }

        public string State_Compare { get; set; }

        public string PostalCode_Compare { get; set; }

        public string Country_Compare { get; set; }

        #endregion
    }
}
