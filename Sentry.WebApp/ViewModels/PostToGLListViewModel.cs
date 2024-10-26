using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class PostToGLListViewModel : BaseListViewModel
    {
        public PostToGLListViewModel() : base() { }

        public List<PostToGLRemediationListItemViewModel> RemediationList { get; set; }
    }
}
