using System.ComponentModel.DataAnnotations;
using System;

namespace Sentry.WebApp.Data.Models.GiftTransmittal
{
    public class PDF
    {
        [Display(Name = "Gift Transmittal ID")]
        public Guid GiftTransmittalID { get; set; }


        [Display(Name = "Gift Transmittal ID")]
        public string Message { get; set; }

        public string Organization { get; set; }

        public string LinkSource { get; set; }


        public string FormNumber { get; set; }
        public string FileName { get; set; }

        public int UA_LOGO { get; set; }

        public int FONT_HELVELTICA { get; set; }

        public int FONT_HELVETICA_BOLD { get; set; }

        public int FONT_HELVETICA_BOLD_OBLIQUE { get; set; }

        public int FONT_HELVETICA_OBLIQUE { get; set; }

        public int FONT_TIMES_ROMAN { get; set; }

        public int FONT_TIMES_BOLD { get; set; }

        public int FONT_TIMES_ITALIC { get; set; }

        public int FONT_TIMES_BOLD_ITALIC { get; set; }

        public int TRANS_TABLE { get; set; }

        public int ROW_HEIGHT { get; set; }

        public int RIGHT_MARGIN = 15;
        public int NAME_COL_WIDTH = 120;
        public int APPEAL_COL_WIDTH = 60;
        public int FUND_COL_WIDTH = 164;
        public int NUM_ROWS_FIRST_PAGE = 8;
        public int NUM_ROWS_SECOND_PAGE = 25;

        public int RowIndex { get; set; }
        public class Success
        {
            public bool Approved { get; set; }
            public string FormNumber { get; set; }
            public string Message { get; set; }
        }

    }

}
