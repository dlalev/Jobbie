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
        readonly IPublishEndpoint _publishEndpoint;
        public ClientPostController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        [HttpPost]
        public async Task<ActionResult> CreatePost(string value)
        {
            await _publishEndpoint.Publish< Shared.Models.Models.ClientPost>( new {
            Value = value
            });
            return Ok();
        }
    }
}
