using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLessons.Services.ServiceWorkers
{
    public class GreeterServiceWorker : IHostedService
    {
        private readonly IGreeter _greeter;

        public GreeterServiceWorker(IGreeter greeter)
        {
            _greeter = greeter;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            int i = 1;
            while (i<=100)
            {
                Console.WriteLine(_greeter.Greet("Tom " + i++));
                await Task.Delay(10);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    //public class GreeterServiceWorker2 : IHostedService
    //{
    //    private readonly IGreeter _greeter;

    //    public GreeterServiceWorker2(IGreeter greeter)
    //    {
    //        _greeter = greeter;
    //    }

    //    public async Task StartAsync(CancellationToken cancellationToken)
    //    {
    //        int i = 1;
    //        while (i<= 100)
    //        {
    //            Console.WriteLine(_greeter.Greet("Tom2 " + i++));
    //            await Task.Delay(10);
    //        }
    //    }

    //    public Task StopAsync(CancellationToken cancellationToken)
    //    {
    //        return Task.CompletedTask;
    //    }
    //}
}
