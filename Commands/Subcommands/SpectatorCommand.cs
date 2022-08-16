using Exiled.API.Features;
using Exiled.CustomRoles;
using CommandSystem;
using Exiled.API.Features.Items;
using System;
using CustomRoleHandler = Exiled.CustomRoles.API.Features.CustomRole;

namespace GhostSpectatorReworked.Commands.Subcommands
{
    public class SpectatorCommand : ICommand
    {
        public string Command { get; } = "spectator";

        public string[] Aliases { get; } = { "spec" };

        public string Description { get; } = "Makes the player a spectator again";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player != null)
            {
                if (CustomRoleHandler.Get(99).Check(player))
                {
                    CustomRoleHandler.Get(99).RemoveRole(player);
                }
                response = "Removed role!";
                return true;
            }
            response = "This command cannot be sent through the console at the moment!";
            return false;
        }
    }
}
