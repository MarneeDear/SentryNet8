using Sentry.WebApp.ViewModels.FundsTransfer;
using System.Linq;
using System;

namespace Sentry.WebApp.ViewModels
{
    public class TransferRoutingType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static explicit operator TransferRoutingType(Sentry.Domain.AccountsPayable.Entities.TransferRoutingTypes projectType)
        {
            return new TransferRoutingType
            {
                Id = projectType.Id,
                Name = projectType.RoutingTypeDescription
            };
        }

    }
}
