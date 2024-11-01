﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class DesignationListViewModel : BaseListViewModel
    {
        public DesignationListViewModel() : base() { }

        public List<DesignationRemediationListItemViewModel> RemediationList { get; set; }
    }
}
