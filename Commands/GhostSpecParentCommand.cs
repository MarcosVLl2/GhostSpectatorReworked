using System;
using CommandSystem;
using Exiled.API.Features;
using GhostSpectatorReworked.Commands.Subcommands;

namespace GhostSpectatorReworked.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    public class GhostSpecParentCommand : ParentCommand
    {
        public GhostSpecParentCommand() => LoadGeneratedCommands();
        public override string Command => "ghostspectator";
        public override string Description => null;
        public override string[] Aliases => new string[] { "ghspec", "ghsp" };

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new GhostCommand());
            RegisterCommand(new GhostNoclipCommand());
            RegisterCommand(new TeleportToCommand());
            RegisterCommand(new TeleportToPlayerCommand());
            RegisterCommand(new SpectatorCommand());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "Please, specify a sub command: ghost, spectator, teleportto, teleporttoplayer, ghnoclip";
            return true;
        }
    }
}
