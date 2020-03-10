using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogWire.Agent.Ingress.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace LogWire.Agent.Ingress.RabbitMQ
{
    public class RabbitManager
    {

        private static RabbitManager _instance;

        public static RabbitManager Instance => _instance ?? new RabbitManager();

        private string _exchangeName = "logwire.agent";
        private string _hostname = "localhost";
        private string _username = "guest";
        private string _password = "guest";

        private RabbitManager()
        {

            using (var conn = GetConnection())
            {

                IModel model = conn.CreateModel();

                model.ExchangeDeclare(_exchangeName, ExchangeType.Topic);

                model.Close();
                conn.Close();

            }


        }

        private IConnection GetConnection()
        {

            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = _hostname,
                Password = _password,
                UserName = _username
            };

            return factory.CreateConnection();

        }

        public void Startup(IConfiguration configuration)
        {
            _hostname = configuration["rabbitmq.guest.endpoint"];
            _password = configuration["rabbitmq.guest.pass"];
        }

        public void AddUserAuthenticationEvent(UserAuthenticationRequestModel body)
        {

            using (var conn = GetConnection())
            {

                IModel model = conn.CreateModel();

                byte[] messagebuffer = Encoding.Default.GetBytes(JsonConvert.SerializeObject(body));
                model.BasicPublish(_exchangeName, "event.user.auth", model.CreateBasicProperties(), messagebuffer);

                model.Close();
                conn.Close();

            }

        }
    }
}
