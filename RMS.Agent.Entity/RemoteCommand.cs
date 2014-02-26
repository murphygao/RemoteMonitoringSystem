using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
