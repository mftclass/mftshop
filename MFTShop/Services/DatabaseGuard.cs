using MFTShop.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MFTShop.Services
{
    public class DatabaseGuard : IHostedService, IDisposable
    {
        Timer timer;
        readonly IServiceScopeFactory serviceFactory;
        public DatabaseGuard(IServiceScopeFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
            
        }
        
        public void Dispose()
        {
            timer.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(WatchOrders, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            return Task.CompletedTask;
        }

        private void WatchOrders(object state)
        {
            using var scope = serviceFactory.CreateScope();
            var orderServices = scope.ServiceProvider.GetRequiredService<IOrderServices>();
            var count = orderServices.validateOrders();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Change(int.MaxValue, 0);
            return Task.CompletedTask;
        }
    }
}
