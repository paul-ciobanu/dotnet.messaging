using dotnet.messaging.domain;
using dotnet.messaging.domain.Cache;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.messaging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMessageCache _messageCache;
        private readonly IMessageWriteCache _messageWrite;
        private readonly Random _random = new();

        public MessageController(ILogger<WeatherForecastController> logger, IMessageCache messageCache, IMessageWriteCache messageWrite)
        {
            _logger = logger;
            _messageCache = messageCache;
            _messageWrite = messageWrite;
        }

        [HttpGet(Name = "GetAllMessages")]
        public IEnumerable<Message> Get()
        {
            return _messageCache.GetAll();
        }

        [HttpPost(Name = "PostMessage")]
        public void Post([FromBody] string message)
        {
            _messageWrite.Add(new Message(_random.Next(), message));
        }
    }
}
