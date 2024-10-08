﻿using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TvShop.DatabaseService.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                bool isHealthy = await IsDatabaseConnectionOkAsync();
                return isHealthy
                    ? HealthCheckResult.Healthy("Database connection is OK")
                    : HealthCheckResult.Unhealthy("Database connection ERROR");
            }
            return HealthCheckResult.Unhealthy("Mesaj de oprire");
        }

        private static Task<bool> IsDatabaseConnectionOkAsync()
        {
            return Task.FromResult(DateTime.Now.Millisecond % 2 == 0);
        }
    }
}