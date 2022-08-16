using Exiled.API.Features;
using Exiled.CustomRoles;
using CommandSystem;
using Exiled.API.Features.Items;
using System;
using CustomRoleHandler = Exiled.CustomRoles.API.Features.CustomRole;

namespace GhostSpectatorReworked.Commands.Subcommands
{
    public class GhostNoclipCommand : ICommand
    {
        public string Command { get; } = "ghnoclip";

        public string[] Aliases { get; } = Array.Empty<string>();

        public string Description { get; } = "Toggles Noclip for the GhostSpectator.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player != null)
            {
                if (CustomRoleHandler.Get(99).Check(player))
                {
                    string? onoroff = arguments.At(0);
                    if(onoroff != null)
                    {
                        if(onoroff == "on")
                        {
                            player.NoClipEnabled = true;
                            player.Broadcast(new Exiled.API.Features.Broadcast("Noclip enabled", 5, true), false);
                            response = "Noclip enabled";
                            return true;
                        }
                        else if(onoroff == "off")
                        {
                            player.NoClipEnabled = false;
                            player.Broadcast(new Exiled.API.Features.Broadcast("Noclip enabled", 5, true), false);
                            response = "Noclip disabled";
                            return true;
                        }
                        response = "That is not a valid argument";
                        return false;
                    }
                    response = "Unexpected error occurred";
                    return false;
                }
                response = "You are not in GhostSpectator mode!";
                return false;
            }
            response = "This command cannot be sent through the console at the moment!";
            return false;
        }
    }
}