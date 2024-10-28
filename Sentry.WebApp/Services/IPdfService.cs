using Sentry.WebApp.Data.Models.GiftTransmittal;
using Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal;

namespace Sentry.WebApp.Services
{
    public interface IPdfService
    {
        PDF CreateGiftTransmittalPDF(GiftTransmittal giftTransmittal, string organization);
    }
}
