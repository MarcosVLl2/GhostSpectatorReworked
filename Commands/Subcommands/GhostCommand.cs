using Exiled.API.Features;
using Exiled.CustomRoles;
using CommandSystem;
using Exiled.API.Features.Items;
using System;
using CustomRoleHandler = Exiled.CustomRoles.API.Features.CustomRole;

namespace GhostSpectatorReworked.Commands.Subcommands
{
    public class GhostCommand : ICommand
    {
        public string Command { get; } = "ghost";

        public string[] Aliases { get; } = new[] { "gh" };

        public string Description { get; } = "Become the GhostSpectator role.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if(player != null)
            {
                if (player.Role == RoleType.Spectator)
                {
                    CustomRoleHandler.Get(99)?.AddRole(player);
                }
                response = "Gave the role!";
                return true;
            }
            response = "This command cannot be sent through the console at the moment!";
            return false;
        }
    }
}
