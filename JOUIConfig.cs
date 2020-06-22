using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPlugins.JobsOnlineUI
{
    public class JOUIConfig : IRocketPluginConfiguration
    {
        public bool Enabled;
        public string MedicGroup;
        public string PoliceGroup;
        public void LoadDefaults()
        {
            Enabled = true;
            MedicGroup = "";
            PoliceGroup = "";
        }
    }
}