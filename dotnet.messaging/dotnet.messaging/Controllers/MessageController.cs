using dotnet.messaging.clients.Handlers;
using dotnet.messaging.domain;
using dotnet.messaging.domain.Cache;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.messaging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IMessageCache _messageCache;
        private readonly IMessageProducer _messageProducer;

        public MessageController(ILogger<MessageController> logger, IMessageCache messageCache, IMessageProducer messageProducer)
        {
            _logger = logger;
            _messageCache = messageCache;
            _messageProducer = messageProducer;
        }

        [HttpGet(Name = "GetAllMessages")]
        public IEnumerable<Message> Get()
        {
            return _messageCache.GetAll();
        }

        [HttpPost(Name = "PostMessage")]
        public void Post([FromBody] string message)
        {
            _messageProducer.Send(message);
        }
    }
}
