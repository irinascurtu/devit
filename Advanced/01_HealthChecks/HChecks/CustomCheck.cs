using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthChecks
{
    public class CustomCheck : IHealthCheck
    {
        //public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        //{
        //    throw new System.NotImplementedException();
        //}

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=ProductsCatalog;Integrated Security=SSPI;"))
            {
                try
                {
                    await connection.OpenAsync();
                }
                catch (Exception)
                {
                    return HealthCheckResult.Unhealthy("It sucks....Unhealthy connection");
                }
                return HealthCheckResult.Healthy("healthy connection");
            }
        }
    }
}
