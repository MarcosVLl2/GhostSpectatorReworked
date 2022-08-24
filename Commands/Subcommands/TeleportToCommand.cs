using Exiled.API.Features;
using Exiled.CustomRoles;
using Exiled.Permissions.Extensions;
using CommandSystem;
using Exiled.API.Features.Items;
using System;
using CustomRoleHandler = Exiled.CustomRoles.API.Features.CustomRole;

namespace GhostSpectatorReworked.Commands.Subcommands
{
    public class TeleportToCommand : ICommand
    {
        public string Command { get; } = "teleportto";

        public string[] Aliases { get; } = { "tpto" };

        public string Description { get; } = GhostSpectator.instance.Translation.TPToCommandDescription;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player != null)
            {
                if (!sender.CheckPermission("ghsp.tpto"))
                {
                    if (CustomRoleHandler.Get(99).Check(player))
                    {
                        string? teleportlocation = arguments.At(0);
                        if (teleportlocation != null)
                        {
                            teleportlocation = teleportlocation.ToLower().Trim();
                            if (teleportlocation == "light" || teleportlocation == "lz")
                            {
                                player.NoClipEnabled = true;
                                player.Broadcast(new Exiled.API.Features.Broadcast(GhostSpectator.instance.Translation.NoclipOn, 5, true), false);
                                response = "Noclip enabled";
                                return true;
                            }
                            else if (teleportlocation == "heavy" || teleportlocation == "lz")
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
