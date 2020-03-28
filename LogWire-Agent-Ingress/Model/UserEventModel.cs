using System;

namespace LogWire.Agent.Ingress.Model
{
    public class UserEventModel
    {

        public string EventType { get; set; }
        public string MachineName { get; set; }
        public string MachineIp { get; set; }
        public string Username { get; set; }
        public string EventDate { get; set; }

        public bool IsValid => !String.IsNullOrWhiteSpace(EventType) && !String.IsNullOrWhiteSpace(MachineName)
                                                                     && !String.IsNullOrWhiteSpace(MachineIp) &&
                                                                     !String.IsNullOrWhiteSpace(Username)
                                                                     && !String.IsNullOrWhiteSpace(EventDate);
    }
}
