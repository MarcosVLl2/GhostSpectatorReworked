using System.Collections.Generic;
using Assets._Scripts.Dissonance;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using PlayerHandler = Exiled.Events.Handlers.Player;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using UnityEngine;
using YamlDotNet.Serialization;

namespace GhostSpectatorReworked
{
    [CustomRole(RoleType.Tutorial)]
    public class GhostSpectatorRole : CustomRole
    {
        public override uint Id { get; set; } = 99;
        public RoleType VisibleRole { get; set; } = RoleType.Tutorial;
        public override int MaxHealth { get; set; } = 65535;
        public override string Name { get; set; } = "GhostSpectator";
        public override string Description { get; set; } = GhostSpectator.instance.Translation.GHSpecRoleDescription;
        public override string CustomInfo { get; set; } = "GhostSpectator";
        public override bool KeepInventoryOnSpawn { get; set; } = false;
        public float MovementMultiplier { get; set; } = 2.5f;
        public override Vector3 Scale { get; set; } = Vector3.one;
        public override List<string> Inventory { get; set; } = new List<string>
        {
            $"{ItemType.Coin}",
            $"{ItemType.GrenadeHE}",
            $"{ItemType.GrenadeFlash}",
            $"{ItemType.Flashlight}",
            $"{ItemType.GunCOM15}"
        };
        [YamlIgnore]
        public override RoleType Role { get; set; } = RoleType.Tutorial;
        [YamlIgnore]
        public override List<CustomAbility> CustomAbilities { get; set; } = new List<CustomAbility>();
        [YamlIgnore]
        public override SpawnProperties SpawnProperties { get; set; } = null;
        protected override void RoleAdded(Player player)
        {
            GhostSpectator.instance.GhostSpectatorList.Add(player);
            player.UnitName = "GHSP";

            Timing.CallDelayed(1.5f, () =>
            {
                player.ChangeAppearance(VisibleRole);
                player.ChangeWalkingSpeed(MovementMultiplier);
                player.ChangeRunningSpeed(MovementMultiplier);
                player.IsGodModeEnabled = true;
                player.IsInvisible = true;
                player.NoClipEnabled = true;
            });

            player.Scale = Scale;
            DissonanceUserSetup dissonance = player.GameObject.GetComponent<DissonanceUserSetup>();
            //dissonance.DisableSpeaking(TriggerType.Role, Assets._Scripts.Dissonance.RoleType.Ghost);
            Vector3 spawnPoint = Room.Get(RoomType.Surface).Position;
            Timing.CallDelayed(0.1f, () => { player.Position = spawnPoint; });
            if (!Scp173.TurnedPlayers.Contains(player))
            {
                Scp173.TurnedPlayers.Add(player);
            }

            if (!Scp096.TurnedPlayers.Contains(player))
            {
                Scp096.TurnedPlayers.Add(player);
            }
            base.RoleAdded(player);
        }
        protected override void RoleRemoved(Player player)
        {
            GhostSpectator.instance.GhostSpectatorList.Remove(player);
            player.Scale = Vector3.one;
            Timing.CallDelayed(1.5f, () =>
            {
                player.ChangeWalkingSpeed(1f);
                player.ChangeRunningSpeed(1f);
                player.IsGodModeEnabled = false;
                player.IsInvisible = false;
                player.NoClipEnabled = false;
            });
            if (Scp173.TurnedPlayers.Contains(player))
            {
                Scp173.TurnedPlayers.Remove(player);
            }

            if (Scp096.TurnedPlayers.Contains(player))
            {
                Scp096.TurnedPlayers.Remove(player);
            }
            DissonanceUserSetup dissonance = player.GameObject.GetComponent<DissonanceUserSetup>();
            //dissonance.EnableSpeaking(TriggerType.Role, Assets._Scripts.Dissonance.RoleType.Null);
            base.RoleRemoved(player);
        }
        protected override void SubscribeEvents()
        {
            PlayerHandler.Spawned += OnSpawned;
            PlayerHandler.FlippingCoin += OnFlippedCoin;
            PlayerHandler.ChangingRadioPreset += OnChangingRadioPreset;
            Exiled.Events.Handlers.Server.RespawningTeam += OnRespawningTeam;

            // Stop GhostSpectator in PlayerHandler
            PlayerHandler.TriggeringTesla += OnTriggeringTesla;
            PlayerHandler.ActivatingGenerator += OnActivatingGenerator;
            PlayerHandler.ActivatingWarheadPanel += OnActivatingWarheadPanel;
            PlayerHandler.ActivatingWorkstation += OnActivatingWorkstation;
            PlayerHandler.ClosingGenerator += OnClosingGenerator;
            PlayerHandler.DamagingShootingTarget += OnDamagingShootingTarget;
            PlayerHandler.DroppingAmmo += OnDroppingAmmo;
            PlayerHandler.DroppingItem += OnDroppingItem;
            PlayerHandler.DryfiringWeapon += OnDryfiringWeapon;
            PlayerHandler.EnteringEnvironmentalHazard += OnEnteringEnvironmentalHazard;
            PlayerHandler.Hurting += OnHurting;
            PlayerHandler.InteractingDoor += OnInteractingDoor;
            PlayerHandler.InteractingElevator += OnInteractingElevator;
            PlayerHandler.InteractingLocker += OnInteractingLocker;
            PlayerHandler.InteractingShootingTarget += OnInteractingShootingTarget;
            PlayerHandler.IntercomSpeaking += OnIntercomSpeaking;
            PlayerHandler.MakingNoise += OnMakingNoise;
            PlayerHandler.PickingUpAmmo += OnPickingUpAmmo;
            PlayerHandler.PickingUpArmor += OnPickingUpArmor;
            PlayerHandler.PickingUpItem += OnPickingUpItem;
            PlayerHandler.OpeningGenerator += OnOpeningGenerator;
            PlayerHandler.PlayerDamageWindow += OnPlayerDamageWindow;
            PlayerHandler.ReceivingEffect += OnReceivingEffect;
            PlayerHandler.RemovingHandcuffs += OnRemovingHandcuffs;
            PlayerHandler.SearchingPickup += OnSearchingPickup;
            PlayerHandler.StayingOnEnvironmentalHazard += OnStayingOnEnvironmentalHazard;
            PlayerHandler.StoppingGenerator += OnStoppingGenerator;
            PlayerHandler.ThrowingItem += OnThrowingItem;
            PlayerHandler.UnlockingGenerator += OnUnlockingGenerator;
            PlayerHandler.UsingRadioBattery += OnUsingRadioBattery;

            // Stop GhostSpectatorRole in MapHandler
            Exiled.Events.Handlers.Map.PlacingBulletHole += OnPlacingBulletHole;
            Exiled.Events.Handlers.Map.PlacingBlood += OnPlacingBlood;

            // Stop GhostSpectatorRole in Scp096Handler
            Exiled.Events.Handlers.Scp096.AddingTarget += On096AddingTarget;

            // Stop GhostSpectatorRole in Scp244Handler
            Exiled.Events.Handlers.Scp244.PickingUpScp244 += OnPickingUp244;
            Exiled.Events.Handlers.Scp244.UsingScp244 += OnUsing244;

            // Stop GhostSpectatorRole in Scp330Handler
            Exiled.Events.Handlers.Scp330.InteractingScp330 += OnInteracting330;
            Exiled.Events.Handlers.Scp330.EatingScp330 += OnEating330Candy;
            Exiled.Events.Handlers.Scp330.PickingUpScp330 += OnPickingUp330;

            // Stop GhostSpectatorRole in Scp914Handler
            Exiled.Events.Handlers.Scp914.Activating += OnActivating914;
            Exiled.Events.Handlers.Scp914.ChangingKnobSetting += OnChanging914KnobSetting;
            Exiled.Events.Handlers.Scp914.UpgradingPlayer += OnUpgradingPlayer;
            Exiled.Events.Handlers.Scp914.UpgradingInventoryItem += OnUpgradingInventoryItem;

            // Stop GhostSpectatorRole in WarheadHandler
            Exiled.Events.Handlers.Warhead.Stopping += OnWarheadStopping;
            Exiled.Events.Handlers.Warhead.ChangingLeverStatus += OnWarheadChangingLever;
            Exiled.Events.Handlers.Warhead.Starting += OnWarheadStarting;
            base.SubscribeEvents();
        }
        protected override void UnsubscribeEvents()
        {
            PlayerHandler.Spawned -= OnSpawned;
            PlayerHandler.FlippingCoin -= OnFlippedCoin;
            PlayerHandler.ChangingRadioPreset -= OnChangingRadioPreset;
            Exiled.Events.Handlers.Server.RespawningTeam -= OnRespawningTeam;

            // Stop GhostSpectator in PlayerHandler
            PlayerHandler.TriggeringTesla -= OnTriggeringTesla;
            PlayerHandler.ActivatingGenerator -= OnActivatingGenerator;
            PlayerHandler.ActivatingWarheadPanel -= OnActivatingWarheadPanel;
            PlayerHandler.ActivatingWorkstation -= OnActivatingWorkstation;
            PlayerHandler.ClosingGenerator -= OnClosingGenerator;
            PlayerHandler.DamagingShootingTarget -= OnDamagingShootingTarget;
            PlayerHandler.DroppingAmmo -= OnDroppingAmmo;
            PlayerHandler.DroppingItem -= OnDroppingItem;
            PlayerHandler.DryfiringWeapon -= OnDryfiringWeapon;
            PlayerHandler.EnteringEnvironmentalHazard -= OnEnteringEnvironmentalHazard;
            PlayerHandler.Hurting -= OnHurting;
            PlayerHandler.InteractingDoor -= OnInteractingDoor;
            PlayerHandler.InteractingElevator -= OnInteractingElevator;
            PlayerHandler.InteractingLocker -= OnInteractingLocker;
            PlayerHandler.InteractingShootingTarget -= OnInteractingShootingTarget;
            PlayerHandler.IntercomSpeaking -= OnIntercomSpeaking;
            PlayerHandler.MakingNoise -= OnMakingNoise;
            PlayerHandler.PickingUpAmmo -= OnPickingUpAmmo;
            PlayerHandler.PickingUpArmor -= OnPickingUpArmor;
            PlayerHandler.PickingUpItem -= OnPickingUpItem;
            PlayerHandler.OpeningGenerator -= OnOpeningGenerator;
            PlayerHandler.PlayerDamageWindow -= OnPlayerDamageWindow;
            PlayerHandler.ReceivingEffect -= OnReceivingEffect;
            PlayerHandler.RemovingHandcuffs -= OnRemovingHandcuffs;
            PlayerHandler.SearchingPickup -= OnSearchingPickup;
            PlayerHandler.StayingOnEnvironmentalHazard -= OnStayingOnEnvironmentalHazard;
            PlayerHandler.StoppingGenerator -= OnStoppingGenerator;
            PlayerHandler.ThrowingItem -= OnThrowingItem;
            PlayerHandler.UnlockingGenerator -= OnUnlockingGenerator;
            PlayerHandler.UsingRadioBattery -= OnUsingRadioBattery;

            // Stop GhostSpectatorRole in MapHandler
            Exiled.Events.Handlers.Map.PlacingBulletHole -= OnPlacingBulletHole;
            Exiled.Events.Handlers.Map.PlacingBlood -= OnPlacingBlood;

            // Stop GhostSpectatorRole in Scp096Handler
            Exiled.Events.Handlers.Scp096.AddingTarget -= On096AddingTarget;

            // Stop GhostSpectatorRole in Scp244Handler
            Exiled.Events.Handlers.Scp244.PickingUpScp244 -= OnPickingUp244;
            Exiled.Events.Handlers.Scp244.UsingScp244 -= OnUsing244;

            // Stop GhostSpectatorRole in Scp330Handler
            Exiled.Events.Handlers.Scp330.InteractingScp330 -= OnInteracting330;
            Exiled.Events.Handlers.Scp330.EatingScp330 -= OnEating330Candy;
            Exiled.Events.Handlers.Scp330.PickingUpScp330 -= OnPickingUp330;

            // Stop GhostSpectatorRole in Scp914Handler
            Exiled.Events.Handlers.Scp914.Activating -= OnActivating914;
            Exiled.Events.Handlers.Scp914.ChangingKnobSetting -= OnChanging914KnobSetting;
            Exiled.Events.Handlers.Scp914.UpgradingPlayer -= OnUpgradingPlayer;
            Exiled.Events.Handlers.Scp914.UpgradingInventoryItem -= OnUpgradingInventoryItem;

            // Stop GhostSpectatorRole in WarheadHandler
            Exiled.Events.Handlers.Warhead.Stopping -= OnWarheadStopping;
            Exiled.Events.Handlers.Warhead.ChangingLeverStatus -= OnWarheadChangingLever;
            Exiled.Events.Handlers.Warhead.Starting -= OnWarheadStarting;
            base.UnsubscribeEvents();
        }
        private void OnChangingRadioPreset(ChangingRadioPresetEventArgs ev)
        {
            if (Check(ev.Player))
            {
                switch (ev.NewValue)
                {
                    case 0:
                        {
                            if(Warhead.Status != WarheadStatus.Detonated || Map.IsLczDecontaminated)
                                Timing.CallDelayed(0.01f, () => ev.Player.Teleport(Room.Get(RoomType.LczClassDSpawn)));
                            break;
                        }
                    case 1:
                        {
                            if (Warhead.Status != WarheadStatus.Detonated)
                                Timing.CallDelayed(0.01f, () => ev.Player.Teleport(Room.Get(RoomType.Hcz106)));
                            break;
                        }
                    case 2:
                        {
                            if (Warhead.Status != WarheadStatus.Detonated)
                                Timing.CallDelayed(0.01f, () => ev.Player.Teleport(Room.Get(RoomType.EzIntercom)));
                            break;
                        }
                    case 3:
                        {
                            Timing.CallDelayed(0.01f, () => ev.Player.Teleport(Room.Get(RoomType.Surface)));
                            break;
                        }
                }
            }
        }
        private void OnFlippedCoin(FlippingCoinEventArgs ev)
        {
            if (ev.Player != null)
            {
                if (Check(ev.Player)) Timing.CallDelayed(3.25f, () =>
                {
                    RemoveRole(ev.Player);
                    ev.Player.SetRole(RoleType.Spectator, SpawnReason.ForceClass, false);
                    GhostSpectator.instance.GhostSpectatorList.Remove(ev.Player);
                });
            }
        }
        private void OnSpawned(SpawnedEventArgs ev)
        {
            Timing.CallDelayed(1f, () =>
            {
                if (Check(ev.Player))
                {
                    ev.Player.NoClipEnabled = true;
                    Exiled.API.Features.Items.Radio radio = (Exiled.API.Features.Items.Radio)Item.Create(ItemType.Radio, ev.Player);

                    Exiled.API.Structs.RadioRangeSettings radioRangeSettings = radio.RangeSettings;
                    radioRangeSettings.TalkingUsage = 0;
                    radioRangeSettings.IdleUsage = 0;
                    radio.Give(ev.Player);
                    radio.RangeSettings = radioRangeSettings;
                    radio.Range = RadioRange.Ultra;

                }
            });
        }
        private void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            List<Player> templist;
            int maxntf = GameCore.ConfigFile.ServerConfig.GetInt("maximum_MTF_respawn_amount"), maxci = GameCore.ConfigFile.ServerConfig.GetInt("maximum_CI_respawn_amount");
            templist = ev.Players;
            ev.Players.Clear();
            if (ev.NextKnownTeam == Respawning.SpawnableTeamType.ChaosInsurgency)
            {
                foreach (Player ply in GhostSpectator.instance.GhostSpectatorList)
                {
                    if (ev.Players.Count < maxci)
                    {
                        ev.Players.Add(ply);
                        GhostSpectator.instance.GhostSpectatorList.Remove(ply);
                        RemoveRole(ply);
                    }
                    else
                    {
                        return;
                    }
                }
                templist.Reverse(); templist.RemoveRange(maxci - ev.Players.Count, templist.Count - maxci - ev.Players.Count); templist.Reverse();
                ev.Players.AddRange(templist);
            }
            if (ev.NextKnownTeam == Respawning.SpawnableTeamType.NineTailedFox)
            {
                foreach (Player ply in GhostSpectator.instance.GhostSpectatorList)
                {
                    if (ev.Players.Count < maxntf)
                    {
                        ev.Players.Add(ply);
                        GhostSpectator.instance.GhostSpectatorList.Remove(ply);
                        RemoveRole(ply);
                    }
                    else
                    {
                        return;
                    }
                }
                templist.Reverse(); templist.RemoveRange(0, maxntf - ev.Players.Count); templist.Reverse();
                ev.Players.AddRange(templist);
            }
        }


        // Functions to stop GhostSpecRole from doing things (basically anything)
        private void On096AddingTarget(AddingTargetEventArgs ev)
        {
            if (Check(ev.Target))
            {
                ev.IsAllowed = false;
                ev.EnrageTimeToAdd = 0;
                ev.Target.ShowHint(Player.Dictionary.Count.ToString());
            }
        }
        private void OnActivating914(ActivatingEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnActivatingGenerator(ActivatingGeneratorEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnActivatingWarheadPanel(ActivatingWarheadPanelEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnActivatingWorkstation(ActivatingWorkstationEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnChanging914KnobSetting(ChangingKnobSettingEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnClosingGenerator(ClosingGeneratorEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnDamagingShootingTarget(DamagingShootingTargetEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnDroppingAmmo(DroppingAmmoEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.Amount = 0;
                ev.IsAllowed = false;
            }
        }
        private void OnDroppingItem(DroppingItemEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnDryfiringWeapon(DryfiringWeaponEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnEating330Candy(EatingScp330EventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnEnteringEnvironmentalHazard(EnteringEnvironmentalHazardEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnHurting(HurtingEventArgs ev)
        {
            if (Check(ev.Attacker))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnInteracting330(InteractingScp330EventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnInteractingElevator(InteractingElevatorEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnInteractingLocker(InteractingLockerEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnInteractingShootingTarget(InteractingShootingTargetEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnIntercomSpeaking(IntercomSpeakingEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnMakingNoise(MakingNoiseEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnOpeningGenerator(OpeningGeneratorEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnPickingUp244(PickingUpScp244EventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnPickingUp330(PickingUpScp330EventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnPickingUpAmmo(PickingUpAmmoEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnPickingUpArmor(PickingUpArmorEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnPlacingBlood(PlacingBloodEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnPlacingBulletHole(PlacingBulletHole ev)
        {
            if (Check(ev.Owner))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnPlayerDamageWindow(DamagingWindowEventArgs ev)
        {
            if (ev.Handler.Attacker != null)
            {
                if (Check(ev.Handler.Attacker))
                {
                    ev.IsAllowed = false;
                }
            }
        }
        private void OnReceivingEffect(ReceivingEffectEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnRemovingHandcuffs(RemovingHandcuffsEventArgs ev)
        {
            if (Check(ev.Cuffer))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnSearchingPickup(SearchingPickupEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnSpawningRagdoll(SpawningRagdollEventArgs ev)
        {
            if (Check(ev.Owner))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnStoppingGenerator(StoppingGeneratorEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnStayingOnEnvironmentalHazard(StayingOnEnvironmentalHazardEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnThrowingItem(ThrowingItemEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnToggleFlashlight(TogglingFlashlightEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsInIdleRange = false;
                ev.IsTriggerable = false;
                ev.IsInHurtingRange = false;
            }
        }
        private void OnUnlockingGenerator(UnlockingGeneratorEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnUsing244(UsingScp244EventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnUsingRadioBattery(UsingRadioBatteryEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnUpgradingInventoryItem(UpgradingInventoryItemEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnUpgradingPlayer(UpgradingPlayerEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnWarheadChangingLever(ChangingLeverStatusEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnWarheadStarting(StartingEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnWarheadStopping(StoppingEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.IsAllowed = false;
            }
        }
    }
    public class Roles
    {
        public List<GhostSpectatorRole> GhostSpectators { get; set; } = new List<GhostSpectatorRole>
        {
            new GhostSpectatorRole()
        };
    }
}
