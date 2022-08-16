using Exiled.API.Features;
using Exiled.CustomRoles;
using CommandSystem;
using Exiled.API.Features.Items;
using System;

namespace GhostSpectatorReworked.Commands.Subcommands
{
    public class TeleportToPlayerCommand : ICommand
    {
        public string Command { get; } = "teleportto";

        public string[] Aliases { get; } = { "tpto" };

        public string Description { get; } = "Teleports to a player that is alive.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "This command is currently in WIP...";
            return true;
        }
    }
}
