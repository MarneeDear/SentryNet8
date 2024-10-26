using Sentry.WebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels.Administration
{
    public class SourceSystem
    {
        public string DisplayName { get; set; }
        public int SystemId { get; set; }
        public int IntegrationId { get; set; }
    }

    public class Category
    {
        public string DisplayName { get; set; }
        public string Id { get; set; }
        public IEnumerable<SourceSystem> SourceSystems { get; set; }
    }

    public class Entity
    {
        public Entity()
        {

        }
        public Entity(IEnumerable<CategorySystemIntegration> categorySystemIntegrations)
        {
            BuildCategories(categorySystemIntegrations).RunSynchronously();
        }

        public string Name { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        private async Task BuildCategories(IEnumerable<CategorySystemIntegration> categorySystemIntegrations)
        {
            var categoryIds = categorySystemIntegrations.Select(c => new { c.CategoryId, c.CategoryDisplayName }).Distinct();
            var categories = new List<Category>();
            foreach (var category in categoryIds)
            {
                var sourceSystems = new List<SourceSystem>();

                foreach (var system in categorySystemIntegrations.Where(s => s.CategoryId == category.CategoryId))
                {
                    sourceSystems.Add(
                        new SourceSystem()
                        {
                            DisplayName = system.SystemName,
                            SystemId = system.SystemId,
                            IntegrationId = system.IntegrationId
                        }
                    );
                }
                categories.Add(
                        new Category()
                        {
                            DisplayName = category.CategoryDisplayName,
                            Id = category.CategoryId,
                            SourceSystems = sourceSystems
                        }
                    );
            }
            this.Categories = categories;
            await Task.CompletedTask;
        }
    }

    public class IntegrationHealthViewModel : BaseViewModel
    {
        public IntegrationHealthViewModel() : base() { }

        public IEnumerable<Entity> Entities { get; set; }
    }

    [Table("IntegrationHealth", Schema = "Integration")]
    public class SystemIntegrationHealth
    {
        public int SourceSystemId { get; set; }
        public int UnprocessedRecords { get; set; }
        public int UnmasteredRecords { get; set; }
        public int UnpromotedRecords { get; set; }
        public bool HistoryInsertTriggerEnabled { get; set; }
        public bool StageInsertTriggerEnabled { get; set; }
        public bool GoodInsertTriggerEnabled { get; set; }
        public bool BadInsertTriggerEnabled { get; set; }
    }
}
