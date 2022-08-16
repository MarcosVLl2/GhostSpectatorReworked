using System.ComponentModel;
using Exiled.API.Interfaces;

namespace GhostSpectatorReworked
{
    public sealed class Config : IConfig
    {
        [Description("Plugin enabled?")]
        public bool IsEnabled { get; set; } = true;
    }
}