using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("VSECategories", Schema = "MDS")]
    public class VSECategory
    {
        [Column("Id")]
        public string Id { get; set; }

        [Column("VSECategoryName")]
        public string VSECategoryName { get; set; }
    }
}
