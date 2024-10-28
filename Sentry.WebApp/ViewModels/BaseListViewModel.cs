using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.ViewModels
{
	public class BaseListViewModel : BaseViewModel
	{
        public BaseListViewModel() : base() { }

        public string Integration { get; set; }
        public int IntegrationId { get; set; }

    }
}