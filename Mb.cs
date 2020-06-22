using System;
using Rocket.Core.Plugins;
using SDG.Unturned;
using Logger = Rocket.Core.Logging.Logger;
using Rocket.Unturned.Player;
using Rocket.Unturned;
using Rocket.Core;
using System.Linq;
using Rocket.Core.Commands;
using Rocket.API;
using UnityEngine;
using System.Text;
using System.Security.Policy;

namespace XPlugins.JobsOnlineUI
{
    public class Main : RocketPlugin<JOUIConfig>
    {
        public int Civil;
        public int Medic;
        public int Police;

        protected override void Load()
        {
            Logger.Log("###################################", ConsoleColor.Green);
            Logger.Log("#[JobsOnline UI] Loaded correctly!#", ConsoleColor.Green);
            Logger.Log("###################################", ConsoleColor.Green);

            U.Events.OnPlayerConnected += OnPlayerEnter;
            U.Events.OnPlayerDisconnected += OnPlayerQuit;
            PlayerInput.onPluginKeyTick += OnKeyPressed;

            if (Configuration.Instance.Enabled)
            {
                Logger.Log("[JobsOnlineUI] Enabled correctly!");
            }
            else
            {
                U.Events.OnPlayerConnected -= OnPlayerEnter;
                U.Events.OnPlayerDisconnected -= OnPlayerQuit;
                PlayerInput.onPluginKeyTick -= OnKeyPressed;
                Logger.Log("[JobsOnlineUI] Its not enabled!");
            }
        }

        private void OnKeyPressed(Player player, uint simulation, byte key, bool state)
        {
            UnturnedPlayer user = UnturnedPlayer.FromPlayer(player);

            if (key == 4 && state)
            {
                EffectManager.sendUIEffect(28000, 280, user.CSteamID, true, Civil+"", Police+"", Medic+"");
            }
            else if (key == 4 && !state)
            {
                EffectManager.askEffectClearByID(28000, user.CSteamID);
            }
        }

        private void OnPlayerEnter(UnturnedPlayer player)
        {
            Civil = Civil + 1;
            var medic = R.Permissions.GetGroup(Configuration.Instance.MedicGroup);
            var police = R.Permissions.GetGroup(Configuration.Instance.PoliceGroup);

            if (player.IsAdmin)
            {
                return;
            }
            else if (medic.Members.Contains(player.Id))
            {
                Medic = Medic + 1;
            }
            else if (police.Members.Contains(player.Id))
            {
                Police = Police + 1;
            }
        }

        private void OnPlayerQuit(UnturnedPlayer player)
        {
            Civil = Civil - 1;
            var medic = R.Permissions.GetGroup(Configuration.Instance.MedicGroup);
            var police = R.Permissions.GetGroup(Configuration.Instance.PoliceGroup);

            if (player.IsAdmin)
            {
                return;
            }
            else if (medic.Members.Contains(player.Id))
            {
                Medic = Medic - 1;
            }
            else if (police.Members.Contains(player.Id))
            {
                Police = Police - 1;
            }
        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= OnPlayerEnter;
            U.Events.OnPlayerDisconnected -= OnPlayerQuit;
            PlayerInput.onPluginKeyTick -= OnKeyPressed;
            Logger.Log("#####################################", ConsoleColor.Red);
            Logger.Log("#[JobsOnline UI] Unloaded correctly!#", ConsoleColor.Red);
            Logger.Log("#####################################", ConsoleColor.Red);
        }
    }
}