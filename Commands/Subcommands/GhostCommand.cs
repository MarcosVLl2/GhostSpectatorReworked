using Exiled.API.Features;
using Exiled.CustomRoles;
using Exiled.Permissions.Extensions;
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

        public string Description { get; } = GhostSpectator.instance.Translation.GhostCommandDescription;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if(player != null)
            {
                if (!sender.CheckPermission("ghsp.ghost"))
                {
                    if (player.Role == RoleType.Spectator)
                    {
                        CustomRoleHandler.Get(99)?.AddRole(player);
                    }
                    response = GhostSpectator.instance.Translation.AddedRole;
                    return true;
                }
                response = GhostSpectator.instance.Translation.NotGhostSpectatorRole;
                return false;
            }
            throw new NullReferenceException("The player was null!");
        }
    }
}
