using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    public class AjaxDataTableRequest
    {
        [Key]
        public long recordId { get; set; }

        public int systemId { get; set; }

        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public string columns { get; set; }
        public string order { get; set; }
        public string search { get; set; }

        [Column("order[0][column]")]
        public string sortColumn { get; set; }

        [Column("order[0][dir]")]
        public string sortColumnDirection { get; set; }

        [Column("search[value]")]
        public string searchValue { get; set; }

        [Column("search[regex]")]
        public string searchRegex { get; set; }

    }
}
