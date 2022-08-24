using Exiled.API.Features;
using Exiled.CustomRoles;
using Exiled.Permissions.Extensions;
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

        public string Description { get; } = GhostSpectator.instance.Translation.SpectatorCommandDescription;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player != null)
            {
                if (!sender.CheckPermission("ghsp.spectator"))
                {
                    if (CustomRoleHandler.Get(99).Check(player))
                    {
                        CustomRoleHandler.Get(99).RemoveRole(player);
                    }
                    response = GhostSpectator.instance.Translation.RemovedRole;
                    return true;
                }

            }
            throw new NullReferenceException("The player was null!");
        }
    }
}
