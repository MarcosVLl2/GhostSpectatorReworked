using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Exiled.API.Enums;
using MEC;

namespace GhostSpectatorReworked
{
    internal sealed class EventHandlers
    {
        
        private static readonly Config config = GhostSpectator.instance.Config;
        public void OnWarheadDetonated()
        {
            GhostSpectator.instance.GhostSpectatorList.ForEach(e =>
            {
                if(e != null && (e.Zone == ZoneType.HeavyContainment || e.Zone == ZoneType.LightContainment || e.Zone == ZoneType.Entrance))
                    Timing.CallDelayed(0.01f, () => e.Teleport(Room.Get(RoomType.Surface)));
            });
        }
        public void OnLightDecontamination(DecontaminatingEventArgs _)
        {
            GhostSpectator.instance.GhostSpectatorList.ForEach(e =>
            {
                if(e != null && e.Zone == ZoneType.LightContainment)
                    Timing.CallDelayed(3f, () => e.Teleport(Room.Get(RoomType.HczEzCheckpoint)));
            });
        }
    }
}