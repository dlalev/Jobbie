using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Post.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientPostController : ControllerBase
    {
        private readonly IBus _busService;
        public ClientPostController(IBus busService)
        {
            _busService = busService;
        }
        [HttpPost]
        public async Task<string> CreatePost(Shared.Models.Models.ClientPost post)
        {
            if (post != null)
            {
                post.AddedOnDate = DateTime.Now;
                Uri uri = new Uri("rabbitmq://localhost/postQueue");
                var endPoint = await _busService.GetSendEndpoint(uri);
                await endPoint.Send(post);
                return "true";
            }
            return "false";
        }
    }
}
