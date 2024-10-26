using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sentry.WebApp.Data.Models;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace Sentry.WebApp.Data
{
    public class DwDbContext : DbContext
    {
        public DbSet<ThroughputMetric> ThroughputMetrics { get; set; }
        public DbSet<DataQualityMetric> DataQualityMetrics { get; set; }

        public DwDbContext(DbContextOptions<DwDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public IEnumerable<ThroughputMetric> GetThroughputMetrics()
        {
            return ThroughputMetrics.FromSqlRaw(
                "EXEC [UAF].[GetIntegrationThroughputMetrics]"
            ).ToList();
        }

        public IEnumerable<DataQualityMetric> GetDataQualityMetrics()
        {
            return DataQualityMetrics.FromSqlRaw(
                "EXEC [UAF].[GetIntegrationDataQualityMetrics]"
            ).ToList();
        }
    }
}
