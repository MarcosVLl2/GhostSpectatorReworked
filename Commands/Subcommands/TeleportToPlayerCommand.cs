using Exiled.API.Features;
using Exiled.CustomRoles;
using Exiled.Permissions.Extensions;
using CommandSystem;
using Exiled.API.Features.Items;
using System;

namespace GhostSpectatorReworked.Commands.Subcommands
{
    public class TeleportToPlayerCommand : ICommand
    {
        public string Command { get; } = "teleporttoplayer";

        public string[] Aliases { get; } = { "tptoplayer" };

        public string Description { get; } = GhostSpectator.instance.Translation.TPToPlayerCommandDescription;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "This command is currently in WIP...";
            return true;
        }
    }
}
