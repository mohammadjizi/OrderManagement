using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Confluent.Kafka;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Order.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{

    private readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetOrder")]
    public IEnumerable<Order> Get()
    {

        List<Order> orders = new List<Order>();
        for (int i = 0; i < 5; i++)
        {
            orders.Add(new Order() { Id = i.ToString(), Name = ("Order_" + i.ToString()) });
        }
        return orders;
    }

    [HttpPost(Name = "PostOrder")]
    public async void Post(int count)
    {
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

        using (var p = new ProducerBuilder<Null, string>(config).Build())
        {
            //try
            //{

                for (int i = 0; i < count; i++)
                {

                    var order = JsonSerializer.Serialize(new Order()
                    { Id = i.ToString(), Name = ("Order_" + i.ToString()) });

                    var dr = await p.ProduceAsync("order",
                    new Message<Null, string> { Value = order });
                }
            //}
            //catch (ProduceException<Null, string> e)
            //{

            //}
        }
    }

}
