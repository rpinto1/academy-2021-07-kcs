using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.DailyUpdates
{
    public sealed class WindowsBackgroundService : BackgroundService
    {
        private readonly UpdatePrice _updatePrice;
        private readonly ILogger<WindowsBackgroundService> _logger;

        public WindowsBackgroundService(
            UpdatePrice updatePrice,
            ILogger<WindowsBackgroundService> logger) =>
            (_updatePrice, _logger) = (updatePrice, logger);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _updatePrice.UpdatePrices();
                    await _updatePrice.UpdatePricesQFS();

                    await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }

    }
}
