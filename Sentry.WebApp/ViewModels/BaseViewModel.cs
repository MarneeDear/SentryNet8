using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Sentry.WebApp.ViewModels
{
    //public class Category
    //{
    //    public string DisplayName { get; set; }
    //    public string Id { get; set; }
    //    public List<SourceSystem> SourceSystems { get; set; }
    //}

    //public class SourceSystem
    //{
    //    public string DisplayName { get; set; }
    //    public int SystemId { get; set; }
    //    public int IntegrationId { get; set; }
    //}

    public class BaseViewModel
	{
        public BaseViewModel() { }
            
        public string User { get; set; }
        public string Title { get; set; }
		public string PageId { get; set; }
		public string PageWrapperClass { get; set; }
        public string ParentActiveClass { get; set; }
        public string ActiveClass { get; set; }
		public string Message { get; set; }
        
        //public IntegrationRemediationCountViewModel RemediationCounts { get; set; }

        public List<NavigationGroup> NavigationGroups { get; set; }

        public string CurrentRole { get; set; }
        public IEnumerable<SelectListItem> AlternateApprovers { get; set; }
        public bool AllowAlternateApprover { get; set; }
        public virtual bool IsValid()
        {
            return true;
        }
    }

    public class NavigationGroups
    {
        public List<NavigationGroup> NavigationGroup { get; set; }
    }

    public class NavigationGroup
    {
        public string Name { get; set; }

        public List<NavigationItem> NavigationItem { get; set; }

        public bool Accessible
        {
            get
            {
                if (NavigationItem == null)
                    return false;

                return NavigationItem.Any(i => i.Accessible);
            }
        }

        public int RemediationCount
        {
            get
            {
                if (NavigationItem == null)
                    return 0;

                return NavigationItem.Sum(i => i.RemediationCount);
            }
        }
    }

    public class NavigationItem
    {
        public string Name { get; set; }

        public bool Accessible { get; set; }

        public int RemediationCount { get; set; }
    }
}
