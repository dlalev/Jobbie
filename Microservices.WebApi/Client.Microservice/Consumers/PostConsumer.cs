using MassTransit;
using Shared.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Microservice.Consumer
{
    public class PostConsumer : IConsumer<ClientPost>
    {
        public async Task Consume(ConsumeContext<ClientPost> context)
        {
            await Task.Run(() => { var obj = context.Message; });
        }
    }
}