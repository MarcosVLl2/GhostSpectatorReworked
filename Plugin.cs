using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.CustomRoles;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;

namespace GhostSpectatorReworked
{
    public class GhostSpectator : Plugin<Config>
    {
        public List<Player> GhostSpectatorList = new();
        public static GhostSpectator instance { get; private set; }
        private EventHandlers Eventhandler { get; set; }
        public override void OnEnabled()
        {
            instance = this;
            Eventhandler = new EventHandlers();
            RegisterEvents();
            Log.Warn("¡Gracias por usar mi plugin! Cualquier bug o sugerencia notificar a: \"nombre_original#8857\"");
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            UnregisterEvents();
            Eventhandler = null;
            instance = null;
            base.OnDisabled();
        }
        private void RegisterEvents()
        {
            Exiled.Events.Handlers.Player.ChangingRole += Eventhandler.OnChangingRole;
            Exiled.Events.Handlers.Warhead.Detonated += Eventhandler.OnWarheadDetonated;
            Exiled.Events.Handlers.Map.Decontaminating += Eventhandler.OnLightDecontamination;
            CustomRole.RegisterRoles(overrideClass: new Roles());
        }
        private void UnregisterEvents()
        {
            CustomRole.UnregisterRoles();
            Exiled.Events.Handlers.Player.ChangingRole -= Eventhandler.OnChangingRole;
            Exiled.Events.Handlers.Warhead.Detonated -= Eventhandler.OnWarheadDetonated;
            Exiled.Events.Handlers.Map.Decontaminating -= Eventhandler.OnLightDecontamination;
        }
        public override string Name => "GhostSpectatorReworked";
        public override string Author => "MarcosVLl2";
        public override Version Version { get; } = new Version(0, 0, 13);
        public override Version RequiredExiledVersion { get; } = new Version(5, 3, 0);
    }
}