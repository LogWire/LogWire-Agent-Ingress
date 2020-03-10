namespace LogWire.Agent.Ingress.Model
{
    public class UserAuthenticationRequestModel
    {

        public string EventType { get; set; }
        public string MachineName { get; set; }
        public string MachineIp { get; set; }
        public string Username { get; set; }
        public string EventDate { get; set; }


    }
}
