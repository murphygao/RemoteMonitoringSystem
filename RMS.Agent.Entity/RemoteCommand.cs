using System.Runtime.Serialization;

namespace RMS.Agent.Entity
{
    [DataContract]
    public class RemoteCommand
    {
        private string commandLine;

        [DataMember]
        public string CommandLine
        {
            get { return commandLine; }
            set { commandLine = value; }
        }
    }
}
