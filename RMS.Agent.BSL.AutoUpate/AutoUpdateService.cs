using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Agent.BSL.AutoUpate
{
    public class AutoUpdateService
    {

        public void Command(string type)
        {
            switch (type)
            {
                case "all":
                    UpdateAgentAll();
                    break;
                case "dll":
                    UpdateAgentDLLOnly();
                    break;
                case "others":
                    UpdateOthers();
                    break;
            }
        }


        private void UpdateAgentAll()
        {
            

        }

        private void UpdateAgentDLLOnly()
        {
            

        }

        private void UpdateOthers()
        {
            

        }
    }
}
