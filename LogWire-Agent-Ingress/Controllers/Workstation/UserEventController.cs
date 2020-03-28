using LogWire.Agent.Ingress.Model;
using LogWire.Agent.Ingress.RabbitMQ;
using Microsoft.AspNetCore.Mvc;

namespace LogWire.Agent.Ingress.Controllers.Workstation
{
    [ApiController]
    public class UserEventController : ControllerBase
    {

        [HttpPost]
        [Route("/workstation/user/event/authentication")]
        public IActionResult LogUserEvent([FromBody] UserEventModel body)
        {

            if (body != null && body.IsValid)
            {
                RabbitManager.Instance.AddUserAuthenticationEvent(body);

                return Ok();
            }

            return BadRequest();
        }

    }
}