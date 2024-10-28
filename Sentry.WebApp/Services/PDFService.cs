using Sentry.WebApp.Data.Models.GiftTransmittal;
using Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using iText.Layout.Element;
using System.Globalization;

namespace Sentry.WebApp.Services
{
    public class PDFService : IPdfService
    {
        private readonly Config _config;
        private readonly ILogger<PDFService> _logger;
        private readonly IDomainService _domainService;
        private readonly IWebHostEnvironment _env;

        public PDFService(IOptions<Config> config,
            IDomainService domainService,
            IWebHostEnvironment env,
            ILogger<PDFService> logger)
        {
            _config = config.Value;
            _domainService = domainService;
            _env = env;
            _logger = logger;

        }

        public PDF CreateGiftTransmittalPDF(GiftTransmittal giftTransmittal, string organization)
        {
            PDF model = new PDF();

            model.Organization = organization;
            // Update the path so that it points to the DLL on your machine
            DebenuPDFLibraryAX1511.PDFLibrary qp = new DebenuPDFLibraryAX1511.PDFLibrary();

            // Create an instance of the class and give it the path to the DLL
            qp.UnlockKey(_config.UAFForms.UnlockKey);

            if (qp.Unlocked() == 0)
            {
                model.Message = ("License unlock failed");
                throw new Exception($"Error unlocking the PDF Library");
            }

            model.LinkSource = giftTransmittal.Id.ToString();

            if (giftTransmittal == null)
            {
                model.Message = ("Gift Transmittal not found");
                throw new Exception($"Gift Transmittal {giftTransmittal.FormNumber} not found");
            }

            foreach (var item in giftTransmittal.GiftTransmittalItems)
            {
                item.GiftTransmittalItemRecognitionCredits = item.GiftTransmittalItemRecognitionCredits.Where(c => !c.IsDeleted).ToList();
            }

            // Setup the output file name
            var webRoot = _env.WebRootPath;

            //TODO: change this file path
            //string fileName = "";
            var fileName = $@"{webRoot}\documents\{giftTransmittal.FormNumber}-PrintedForm.pdf";
            //webRoot + "\\documents\\" + giftTransmittal.FormNumber + ".pdf";

            DrawFirstPage(qp, giftTransmittal, organization, ref model);

            //TODO: 
            //  Send to Print Preview

            //save the file
            int returnValue = qp.SaveToFile(fileName);
            _logger.LogTrace($"SaveToFile returned {returnValue}");
            if (System.IO.File.Exists(fileName))
            {
                _logger.LogTrace("Saved PDFDocument", fileName);
            }
            else
            {
                _logger.LogError("Save PDFDocument FAILED", fileName);
            }

            //Need this to properly display link
            model.FormNumber = giftTransmittal.FormNumber;
            model.GiftTransmittalID = giftTransmittal.Id;
            model.FileName = fileName;

            return model;
        }

        private void DrawFirstPage(DebenuPDFLibraryAX1511.PDFLibrary qp, GiftTransmittal gt, string organization, ref PDF model)
        {
            //The initial page, which has a different layout from the other pages.

            qp.SetOrigin(1);

            qp.SetPageSize("letter landscape");

            SetFonts(qp, ref model);

            DrawFirstPageHeader(qp, gt, ref model);

            DrawBatchDetails(qp, gt, _logger, ref model);

            qp.SetFillColor(0, 0, 0);
            qp.DrawBox(model.RIGHT_MARGIN, 320, 685 + 75, 15, 2);
            qp.SetTextColor(1, 1, 1);
            qp.DrawText(330, 330, "Transaction Log");

            SetupTransactionTable(qp, ref model);

            qp.SetTextColor(0, 0, 0);

            model.RowIndex = 2;
            var TotalRowCount = 0;

            for (int i = 0; i < gt.GiftTransmittalItems.Count(); i++)
            {
                //Loop through distributions.

                var item = gt.GiftTransmittalItems.ElementAt(i);
                if (!item.IsDeleted)
                {
                    qp.AppendTableRows(model.TRANS_TABLE, model.RowIndex);
                    DrawGTItem(qp, item, ref model);

                    for (int d = 0; d < item.GiftTransmittalItemDistributions.Count(); d++)
                    {
                        var dist = item.GiftTransmittalItemDistributions.ElementAt(d);
                        if (!dist.IsDeleted)
                        {
                            string fundDesc;
                            if (dist.IsNewFund)
                            {
                                fundDesc = "00-00-0000 - New Fund";
                            }
                            else
                            {
                                var fundResult = _domainService.MasterDataWebService.GetDesignation(organization, dist.FundAccount).Result;
                                fundDesc = dist.FundAccount + " - " + fundResult.Name;
                            }

                            DrawGTDist(qp, dist, 0, 0, fundDesc, ref model);

                            model.RowIndex++;
                            TotalRowCount++;
                            qp.AppendTableRows(model.TRANS_TABLE, model.RowIndex);

                            if (TotalRowCount == model.NUM_ROWS_FIRST_PAGE && model.RowIndex > model.NUM_ROWS_FIRST_PAGE)
                            {
                                qp.DrawTableRows(model.TRANS_TABLE, model.RIGHT_MARGIN, 340, model.ROW_HEIGHT * (model.RowIndex - 1), 1, model.RowIndex - 1);
                                DrawFooter(qp, gt, ref model);

                                qp.NewPage();
                                qp.SelectPage(qp.PageCount());
                                model.RowIndex = 0;

                                //Draw following page.
                                bool middleOfItem = (d + 1) < item.GiftTransmittalItemDistributions.Count();
                                DrawSecondPage(qp, gt, i, d, middleOfItem, organization, ref model);

                                return;
                            }
                        }
                    }
                }
            }

            qp.DrawTableRows(model.TRANS_TABLE, model.RIGHT_MARGIN, 340, model.ROW_HEIGHT * (model.RowIndex - 1), 1, model.RowIndex - 1);
            DrawFooter(qp, gt, ref model);

            return;

        }

        private void DrawSecondPage(DebenuPDFLibraryAX1511.PDFLibrary qp, GiftTransmittal gt, int currItem, int currDist, bool middleOfItem, string organization, ref PDF model)
        {

            //Draw any subsequent pages.

            qp.SetOrigin(1);

            qp.SetPageSize("letter landscape");

            SetFonts(qp, ref model);

            DrawSecondPageHeader(qp, gt, ref model);

            qp.SetTextColor(0, 0, 0);

            model.RowIndex = 2;
            var TotalRowCount = 0;

            for (int i = 0; i < gt.GiftTransmittalItems.Count(); i++)
            {
                //Loop through items.

                //Return to latest item.
                if (i < currItem) continue;

                //We're not in the middle of an item.  
                //We're moving to the next item.
                if (!middleOfItem && i <= currItem) continue;

                var item = gt.GiftTransmittalItems.ElementAt(i);
                if (!item.IsDeleted)
                {
                    qp.AppendTableRows(model.TRANS_TABLE, model.RowIndex);
                    if (i != currItem)
                    {
                        DrawGTItem(qp, item, ref model);
                    }
                    for (int d = 0; d < item.GiftTransmittalItemDistributions.Count(); d++)
                    {
                        //Loop through distributions.
                        var dist = item.GiftTransmittalItemDistributions.ElementAt(d);

                        if (!dist.IsDeleted)
                        {
                            //We may be in the middle of an item.
                            if (middleOfItem && d <= currDist) continue;
                            middleOfItem = false;


                            string fundDesc;
                            if (dist.IsNewFund)
                            {
                                fundDesc = "00-00-0000 - New Fund";
                            }
                            else
                            {
                                fundDesc = dist.FundAccount + " - " + _domainService.MasterDataWebService.GetDesignation(organization, dist.FundAccount).Result.Name;
                            }

                            DrawGTDist(qp, dist, 0, 0, fundDesc, ref model);

                            model.RowIndex++;
                            TotalRowCount++;
                            qp.AppendTableRows(model.TRANS_TABLE, model.RowIndex);
                            if (TotalRowCount <= model.NUM_ROWS_SECOND_PAGE && model.RowIndex > model.NUM_ROWS_SECOND_PAGE)
                            {
                                qp.DrawTableRows(model.TRANS_TABLE, model.RIGHT_MARGIN, 75, model.ROW_HEIGHT * (model.RowIndex - 1), 1, model.RowIndex - 1);

                                qp.NewPage();
                                qp.SelectPage(qp.PageCount());

                                DrawSecondPageHeader(qp, gt, ref model);

                                model.RowIndex = 2;

                                DrawGTDist(qp, dist, 0, 0, fundDesc, ref model);

                                model.RowIndex++;
                                TotalRowCount++;
                                qp.AppendTableRows(model.TRANS_TABLE, model.RowIndex);
                            }
                        }
                    }
                }
            }

            qp.DrawTableRows(model.TRANS_TABLE, model.RIGHT_MARGIN, 75, model.ROW_HEIGHT * (model.RowIndex - 1), 1, model.RowIndex - 1);

            return;

        }

        private void SetupTransactionTable(DebenuPDFLibraryAX1511.PDFLibrary qp, ref PDF model)
        {
            //Set table specs.
            var amountColumnWidth = 78;
            qp.SetTextSize(10);

            model.TRANS_TABLE = qp.CreateTable(1, 9);
            model.ROW_HEIGHT = 20;

            qp.SetTableColumnWidth(model.TRANS_TABLE, 1, 1, model.NAME_COL_WIDTH);
            qp.SetTableColumnWidth(model.TRANS_TABLE, 2, 2, amountColumnWidth);
            qp.SetTableColumnWidth(model.TRANS_TABLE, 3, 3, amountColumnWidth);
            qp.SetTableColumnWidth(model.TRANS_TABLE, 4, 4, amountColumnWidth);
            qp.SetTableColumnWidth(model.TRANS_TABLE, 5, 5, 40);
            qp.SetTableColumnWidth(model.TRANS_TABLE, 6, 6, amountColumnWidth);
            qp.SetTableColumnWidth(model.TRANS_TABLE, 7, 7, 60);
            qp.SetTableColumnWidth(model.TRANS_TABLE, 8, 8, 65);
            qp.SetTableColumnWidth(model.TRANS_TABLE, 9, 9, model.FUND_COL_WIDTH);

            qp.SetTableRowHeight(model.TRANS_TABLE, 1, 1, model.ROW_HEIGHT);

            qp.SetTableBorderColor(model.TRANS_TABLE, 0, 0, 0, 0);
            qp.SetTableBorderWidth(model.TRANS_TABLE, 0, 1);

            qp.SetTableCellBackgroundColor(model.TRANS_TABLE, 1, 1, 1, 9, 0, 0, 0);
            qp.SetTableCellTextColor(model.TRANS_TABLE, 1, 1, 1, 9, 1, 1, 1);
            qp.SetTableCellAlignment(model.TRANS_TABLE, 1, 1, 1, 9, 4);

            qp.SetTableCellContent(model.TRANS_TABLE, 1, 1, "Name");
            qp.SetTableCellContent(model.TRANS_TABLE, 1, 2, "Amount");
            qp.SetTableCellContent(model.TRANS_TABLE, 1, 3, "Benefit");
            qp.SetTableCellContent(model.TRANS_TABLE, 1, 4, "Receipt");
            qp.SetTableCellContent(model.TRANS_TABLE, 1, 5, "Pledge");
            qp.SetTableCellContent(model.TRANS_TABLE, 1, 6, "UDF");

            qp.SetTableCellContent(model.TRANS_TABLE, 1, 7, "Dean Project");

            qp.SetTableCellContent(model.TRANS_TABLE, 1, 8, "Appeal");
            qp.SetTableCellContent(model.TRANS_TABLE, 1, 9, "Designation");
        }

        private string TrimText(DebenuPDFLibraryAX1511.PDFLibrary qp, string value, int maxWidth)
        {
            //Trim text to max width and append ellipsis.

            if (qp.GetTextWidth(value.Trim() + "...") > maxWidth - 5)
                return TrimText(qp, value.Substring(0, value.Length - 1), maxWidth);
            else
            {
                return value.Trim() + "...";
            }
        }

        private string TranslateUDFReason(int ExemptionID)
        {
            //Set appropriate UDF code.

            if (ExemptionID == 1)
                return "SCH";
            else
                return "OTH";
        }

        private void DrawGTItem(DebenuPDFLibraryAX1511.PDFLibrary qp, GiftTransmittalItem item, ref PDF model)
        {
            //Set table content for item.

            var constituent = String.Empty;

            if (String.IsNullOrWhiteSpace(item.ConstituentOrganizationName))
            {
                constituent = $"{item.ConstituentFirstName} {item.ConstituentLastName}";
            }
            else
            {
                constituent = item.ConstituentOrganizationName;
            }

            qp.AppendTableRows(model.TRANS_TABLE, model.RowIndex);

            if (qp.GetTextWidth(constituent) > model.NAME_COL_WIDTH - 5)
                qp.SetTableCellContent(model.TRANS_TABLE, model.RowIndex, 1, TrimText(qp, constituent, model.NAME_COL_WIDTH));
            else
                qp.SetTableCellContent(model.TRANS_TABLE, model.RowIndex, 1, constituent);

            if (qp.GetTextWidth(item.Appeal) > model.APPEAL_COL_WIDTH - 5)
                qp.SetTableCellContent(model.TRANS_TABLE, model.RowIndex, 8, TrimText(qp, item.Appeal, model.APPEAL_COL_WIDTH));
            else
                qp.SetTableCellContent(model.TRANS_TABLE, model.RowIndex, 8, item.Appeal);
        }

        private void DrawGTDist(DebenuPDFLibraryAX1511.PDFLibrary qp, GiftTransmittalItemDistribution dist, int initialRow, int distCount, string fundDesc, ref PDF model)
        {
            //Set table content for distribution.
            qp.SetTableCellAlignment(model.TRANS_TABLE, model.RowIndex, 2, model.RowIndex, 4, 5);
            qp.SetTableCellAlignment(model.TRANS_TABLE, model.RowIndex, 5, model.RowIndex, 5, 4);
            qp.SetTableCellAlignment(model.TRANS_TABLE, model.RowIndex, 6, model.RowIndex, 6, 5);

            qp.SetTableCellContent(model.TRANS_TABLE, model.RowIndex, 2, string.Format("{0:n}", dist.Amount));
            qp.SetTableCellContent(model.TRANS_TABLE, model.RowIndex, 3, string.Format("{0:n}", dist.BenefitAmount));
            qp.SetTableCellContent(model.TRANS_TABLE, model.RowIndex, 4, string.Format("{0:n}", dist.ReceiptAmount));

            if (dist.IsPledge == true)
                qp.SetTableCellContent(model.TRANS_TABLE, model.RowIndex, 5, "Yes");
            else
                qp.SetTableCellContent(model.TRANS_TABLE, model.RowIndex, 5, "No");

            qp.SetTableCellContent(model.TRANS_TABLE, model.RowIndex, 6, string.Format("{0:n}", dist.UdfFeeAmount));

            if (dist.UdfFeeAmount == 0 &&  dist.UdfFeeExemptionId > 0) qp.SetTableCellContent(model.TRANS_TABLE, model.RowIndex, 6, TranslateUDFReason(dist.UdfFeeExemptionId));
            
            qp.SetTableCellContent(model.TRANS_TABLE, model.RowIndex, 7, string.Format("{0:n}", dist.UdfDeanProject));

            if (qp.GetTextWidth(fundDesc + "...") > model.FUND_COL_WIDTH - 5)
                qp.SetTableCellContent(model.TRANS_TABLE, model.RowIndex, 9, TrimText(qp, fundDesc, model.FUND_COL_WIDTH));
            else
                qp.SetTableCellContent(model.TRANS_TABLE, model.RowIndex, 9, fundDesc);

        }

        private void DrawFirstPageHeader(DebenuPDFLibraryAX1511.PDFLibrary qp, Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal gt, ref PDF model)
        {
            //Set header data for first page format.

            qp.SelectFont(model.FONT_HELVETICA_BOLD);
            qp.SetTextSize(8);

            model.UA_LOGO = qp.AddImageFromFile(_env.WebRootPath + "/img/UAFoundationLogo.jpg", 0);

            qp.SelectImage(model.UA_LOGO);
            qp.DrawImage(model.RIGHT_MARGIN, 20, 200, 50);

            qp.SelectFont(model.FONT_TIMES_ROMAN);
            qp.SetTextSize(8);

            qp.DrawText(model.RIGHT_MARGIN, 75, "\"Swede\" Johnson Bldg., Financial Services Dept.");
            qp.DrawText(model.RIGHT_MARGIN, 85, "1111 N. Cherry Ave., Room 403, P.O. Box 210109");
            qp.DrawText(model.RIGHT_MARGIN, 95, "Tucson, Arizona 85721");

            qp.SetTextSize(10);
            qp.SetFillColor(.85, .85, .85);
            qp.SetTextColor(.55, .55, .55);
            qp.DrawBox(625, 20, 150, 75, 2);
            qp.DrawText(670, 50, "For University of");
            qp.DrawText(665, 60, "Arizona Foundation");
            qp.DrawText(685, 70, "Use Only");

            qp.SelectFont(model.FONT_TIMES_BOLD);
            qp.SetTextColor(0, 0, 0);

            qp.DrawText(355, 45, gt.FormNumber.ToString());

            qp.SetTextSize(14);
            qp.DrawText(335, 95, "Gift Transmittal");
        }

        private void DrawSecondPageHeader(DebenuPDFLibraryAX1511.PDFLibrary qp, Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal gt, ref PDF model)
        {
            //Set header data for subsequent pages.

            qp.SelectFont(model.FONT_TIMES_BOLD);
            qp.SetTextColor(0, 0, 0);

            qp.DrawText(355, 45, gt.FormNumber.ToString());
            qp.DrawText(655, 45, "Page 2");

            qp.AppendTableRows(model.TRANS_TABLE, ++model.RowIndex);

            qp.SelectFont(model.FONT_HELVETICA_BOLD);

            qp.SetFillColor(0, 0, 0);
            qp.DrawBox(model.RIGHT_MARGIN, 55, 685, 15, 2);
            qp.SetTextColor(1, 1, 1);
            qp.DrawText(330, 65, "Transaction Log (Cont.)");

            SetupTransactionTable(qp, ref model);
        }

        private void DrawBatchDetails(DebenuPDFLibraryAX1511.PDFLibrary qp, Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal gt, ILogger _logger, ref PDF model)
        {
            //Set table content for batch details.

            qp.SetTextSize(12);

            qp.SetFillColor(0, 0, 0);
            qp.DrawBox(model.RIGHT_MARGIN, 140, 685 + 75, 15, 2);
            qp.SetTextColor(1, 1, 1);
            qp.DrawText(model.RIGHT_MARGIN, 150, "Contact");

            qp.SetFillColor(0, 0, 0);
            qp.DrawBox(model.RIGHT_MARGIN, 190, 685 + 75, 15, 2);
            qp.SetTextColor(1, 1, 1);
            qp.DrawText(model.RIGHT_MARGIN, 200, "Batch Details");

            qp.SelectFont(model.FONT_HELVETICA_BOLD);
            qp.SetTextColor(0, 0, 0);

            qp.DrawText(model.RIGHT_MARGIN, 130, "Prepared By: ");
            qp.DrawText(600, 130, "Date: ");

            qp.DrawText(model.RIGHT_MARGIN, 175, "Name: ");
            qp.DrawText(290, 175, "Email: ");
            qp.DrawText(600, 175, "Phone: ");

            qp.SetTextSize(10);

            qp.DrawText(model.RIGHT_MARGIN, 220, "Trans. Type: ");
            if (gt.BatchTypeCode == 4)
            {
                qp.DrawText(model.RIGHT_MARGIN, 230, "Other Reason: ");
            }

            qp.DrawText(230, 220, "Num. Items: ");

            //Soft credit/package/BFE flag.
            bool BFEProjects = false;
            gt.GiftTransmittalItems.ToList()
                .ForEach(i => i.GiftTransmittalItemDistributions.ToList()
                .ForEach(d =>
                {
                    if (!BFEProjects) BFEProjects = CheckBFEFlag(d.FundAccount, _logger);
                }));
            if (gt.GiftTransmittalItems.Count(i => (i.GiftTransmittalItemRecognitionCredits.Any()) || (i.Package != null && i.Package != "")) > 0) qp.DrawText(230, 260, "*Soft Credit/Package");
            if (BFEProjects) qp.DrawText(345, 260, "*BFE");

            qp.DrawText(model.RIGHT_MARGIN, 260, "Additional Comments: ");
            qp.SetFillColor(.85, .85, .85);
            qp.DrawBox(model.RIGHT_MARGIN, 265, 360, 50, 1);

            qp.SetTextColor(0, 0, 0);

            qp.DrawText(390, 220, "Processing Comments: ");
            qp.SetFillColor(.85, .85, .85);
            qp.DrawBox(390, 225, 385, 90, 1);

            qp.SetTextColor(0, 0, 0);

            qp.SelectFont(model.FONT_HELVELTICA);
            qp.SetTextSize(12);

            DateTime preparedByDate = gt.PreparedByDate;

            qp.DrawText(100, 130, gt.PreparedByName.ToString());
            qp.DrawText(640, 130, preparedByDate.ToShortDateString());

            qp.DrawText(62, 175, gt.ContactName.ToString());
            qp.DrawText(336, 175, gt.ContactEmail?.ToString());
            qp.DrawText(651, 175, gt.ContactPhone?.ToString());

            qp.SetTextSize(10);

            qp.DrawText(90, 220, BatchTypeCodes[gt.BatchTypeCode]);

            if (gt.BatchTypeCode == 4)
            {
                qp.DrawTextBox(90, 220, 130, 25, gt.BatchTypeOtherDesc.ToString(), 1);
            }

            //Total Items.
            qp.DrawText(305, 220, gt.GiftTransmittalItems.Where(d => d.IsDeleted == false).ToArray().Length.ToString());

            if (gt.Comments != null)
            {
                qp.DrawTextBox(20, 270, 340, 270, gt.Comments.ToString(), 1);
            }

            qp.DrawLine(380, 205, 380, 335);

            if (gt.ProcessingComments != null)
            {
                qp.DrawTextBox(395, 230, 360, 270, gt.ProcessingComments.ToString(), 1);
            }
        }

        private bool CheckBFEFlag(string distFundDesc, ILogger _logger)
        {
            bool hasBFEFlag = (bool)_domainService.MasterDataWebService.CheckIfDesignationIsBFE(distFundDesc).Result;
            //bool hasBFEFlag = _context.BFEProjects.Where(i => i.ProjectId == distFundDesc).Count() > 0;
            return hasBFEFlag;
        }

        private void DrawFooter(DebenuPDFLibraryAX1511.PDFLibrary qp, GiftTransmittal gt, ref PDF model)
        {
            //Set content for table footer.

            qp.SetTextSize(10);

            //Add total amounts.
            decimal amountTotal = gt.GiftTransmittalItems
                .Where(s => s.IsDeleted != true).SelectMany(i => i.GiftTransmittalItemDistributions)
                    .Where(d => d.IsDeleted != true).Sum(d => d.Amount);

            decimal benefitTotal = gt.GiftTransmittalItems
                .Where(s => s.IsDeleted != true).SelectMany(i => i.GiftTransmittalItemDistributions)
                .Where(d => d.IsDeleted != true).Sum(d => d.BenefitAmount);
            decimal receiptTotal = gt.GiftTransmittalItems
                .Where(s => s.IsDeleted != true).SelectMany(i => i.GiftTransmittalItemDistributions)
                .Where(d => d.IsDeleted != true).Sum(d => d.ReceiptAmount);
            decimal UDFTotal = gt.GiftTransmittalItems
                .Where(s => s.IsDeleted != true).SelectMany(i => i.GiftTransmittalItemDistributions)
                .Where(d => d.IsDeleted != true).Sum(d => d.UdfFeeAmount);
            decimal UDFDevelopmentTotal = gt.GiftTransmittalItems
                .Where(s => s.IsDeleted != true).SelectMany(i => i.GiftTransmittalItemDistributions)
                .Where(d => d.IsDeleted != true).Sum(d => d.UdfFeeDevelopment.HasValue ? d.UdfFeeDevelopment.Value : 0.0M);
            decimal UDFPresidentTotal = gt.GiftTransmittalItems
                .Where(s => s.IsDeleted != true).SelectMany(i => i.GiftTransmittalItemDistributions)
                .Where(d => d.IsDeleted != true).Sum(d => d.UdfFeePresident.HasValue ? d.UdfFeePresident.Value : 0.0M);
            decimal UDFDeanTotal = gt.GiftTransmittalItems
                .Where(s => s.IsDeleted != true).SelectMany(i => i.GiftTransmittalItemDistributions)
                .Where(d => d.IsDeleted != true).Sum(d => d.UdfFeeDean.HasValue ? d.UdfFeeDean.Value : 0.0M);

            //TODO use the value recordd on the gift transmittal because it is editable and this uses the defaults
            //decimal UDF_Dean = 7777.77M;
            //string DeanUDFProject = String.Empty;

            qp.DrawText(model.RIGHT_MARGIN, 535, "Total");

            qp.DrawText(173, 525, "Amount");
            qp.DrawText(252, 525, "Benefit");
            qp.DrawText(328, 525, "Receipt");
            qp.DrawText(461, 525, "UDF");
            qp.DrawText(516, 525, "UDF Dev.");//"UDF - 4%");
            qp.DrawText(600, 525, "UDF Pres."); //"UDF - 1%");
            qp.DrawText(675, 525, "UDF Dean");
            //qp.DrawText(620, 525, "Dean's Project");

            //Right-justified.
            qp.SetTextAlign(2);

            qp.DrawText(207, 535, string.Format("{0:n}", amountTotal));
            qp.DrawText(283, 535, string.Format("{0:n}", benefitTotal));
            qp.DrawText(362, 535, string.Format("{0:n}", receiptTotal));
            qp.DrawText(480, 535, string.Format("{0:n}", UDFTotal)); 
            //TODO use the value recordd on the gift transmittal because it is editable and this uses the defaults
            qp.DrawText(558, 535, string.Format("{0:n}", UDFDevelopmentTotal));
            qp.DrawText(645, 535, string.Format("{0:n}", UDFPresidentTotal));
            qp.DrawText(722, 535, string.Format("{0:n}", UDFDeanTotal));
            //qp.DrawText(685, 535, DeanUDFProject);

            //Left-justified.
            qp.SetTextAlign(0);

            qp.SetFillColor(0, 0, 0);
            qp.DrawBox(model.RIGHT_MARGIN, 545, 685 + 75, 15, 2);
            qp.SetTextColor(1, 1, 1);
            qp.DrawText(model.RIGHT_MARGIN, 555, "To be completed by the University of Arizona Foundation");

            qp.SetFillColor(1, 1, 1);
            qp.SetTextColor(0, 0, 0);
            qp.DrawBox(model.RIGHT_MARGIN, 560, 685 + 75, 25, 2);
            qp.DrawText(20, 575, "Batch No.:");

            qp.DrawLine(250, 560, 250, 585);
        }

        private void SetFonts(DebenuPDFLibraryAX1511.PDFLibrary qp, ref PDF model)
        {
            model.FONT_HELVELTICA = qp.AddStandardFont(4);
            model.FONT_HELVETICA_BOLD = qp.AddStandardFont(5);
            model.FONT_HELVETICA_BOLD_OBLIQUE = qp.AddStandardFont(6);
            model.FONT_HELVETICA_OBLIQUE = qp.AddStandardFont(7);

            model.FONT_TIMES_ROMAN = qp.AddStandardFont(8);
            model.FONT_TIMES_BOLD = qp.AddStandardFont(9);
            model.FONT_TIMES_ITALIC = qp.AddStandardFont(10);
            model.FONT_TIMES_BOLD_ITALIC = qp.AddStandardFont(11);
        }

        public IDictionary<int, string> BatchTypeCodes
        {
            get
            {
                return new Dictionary<int, string>()
                {
                    { 1, "Check" },
                    { 2, "Credit Card" },
                    { 3, "Cash" },
                    { 4, "Other" },
                    { 5, "Pledge/Gift Commitment" },
                    { 6, "Gift In-Kind" },
                    { 7, "Pledge - Legally Binding" },
                    { 8, "Wire" },
                    { 9, "Property" },
                    { 10, "Stock" },
                    { 11, "Cash/Check" },
                    { 12, "Pledge" },
                    { 13, "Planned Gift" },
                    { 14, "Recurring Gift" },
                    { 15, "Membership" }
                };
            }

        }

    }
}
