using System.ComponentModel;
using Exiled.API.Interfaces;

namespace GhostSpectatorReworked
{
    public sealed class Translation : ITranslation
    {
        [Description("TeleportTo command description")]
        public string TPToCommandDescription { get; private set; } = "Teletransportar a un area especifica.";

        [Description("TeleportToPlayer command description")]
        public string TPToPlayerCommandDescription { get; private set; } = "Teletransportar a un jugador especifico.";

        [Description("GhostSpectatorRole description")]
        public string GHSpecRoleDescription { get; private set; } = "GhostSpectator, mejor espectador en primera persona";

        [Description("Noclip command description")]
        public string NoclipCommandDescription { get; private set; } = "Activar / Desactivar noclip en GhostSpectator.";

        [Description("Noclip turned on text")]
        public string NoclipOn { get; private set; } = "Noclip activado";

        [Description("Noclip turned on text")]
        public string NoclipOff { get; private set; } = "Noclip desactivado";

        [Description("Noclip not understood text")]
        public string NoclipArgNotValid { get; private set; } = "¡Ese argumento no es valido!";

        [Description("User is not GhostSpectatorRole")]
        public string NotGhostSpectatorRole { get; private set; } = "¡No eres GhostSpectator!";

        [Description("Removed role text")]
        public string RemovedRole { get; private set; } = "¡Rol quitado!";

        [Description("Added role text")]
        public string AddedRole { get; private set; } = "¡Rol añadido!";

        [Description("Spectator command description")]
        public string SpectatorCommandDescription { get; private set; } = "Volver a espectador";

        [Description("Ghost command description")]
        public string GhostCommandDescription { get; private set; } = "Ser GhostSpectator";
        [Description("Not enough perms error")]
        public string NotEnoughPerms { get; private set; } = "No tienes permisos suficientes para ejecutar este comando!";
    }
}
