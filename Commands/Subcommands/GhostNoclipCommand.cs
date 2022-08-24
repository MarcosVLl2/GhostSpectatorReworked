using Exiled.API.Features;
using Exiled.Permissions.Extensions;
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

        public string Description { get; } = GhostSpectator.instance.Translation.NoclipCommandDescription;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player != null)
            {
                if (!sender.CheckPermission("ghsp.noclip"))
                {
                    if (CustomRoleHandler.Get(99).Check(player))
                    {
                        string? onoroff = arguments.At(0);
                        if (onoroff != null)
                        {
                            if (onoroff == "on")
                            {
                                player.NoClipEnabled = true;
                                player.Broadcast(new Exiled.API.Features.Broadcast(GhostSpectator.instance.Translation.NoclipOn, 5, true), false);
                                response = "Noclip enabled";
                                return true;
                            }
                            else if (onoroff == "off")
                            {
                                player.NoClipEnabled = false;
                                player.Broadcast(new Exiled.API.Features.Broadcast(GhostSpectator.instance.Translation.NoclipOff, 5, true), false);
                                response = "Noclip disabled";
                                return true;
                            }
                            response = GhostSpectator.instance.Translation.NoclipArgNotValid;
                            return false;
                        }
                        response = GhostSpectator.instance.Translation.NoclipArgNotValid;
                        return false;
                    }
                    response = GhostSpectator.instance.Translation.NotGhostSpectatorRole;
                    return false;
                }
                response = GhostSpectator.instance.Translation.NotEnoughPerms;
                return false;
            }
            throw new NullReferenceException("The player was null!");
        }
    }
}