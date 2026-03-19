using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using System;

namespace NoShoot106
{
    public class Plugin : Plugin<Config>
    {
        public override string Author => "OpasniDed";
        public override string Name => "NoShoot106";
        public override string Prefix => "NoShoot106";
        public override Version Version => new(1, 0, 0);
        public override Version RequiredExiledVersion => new(9, 13, 1, 0);

        public Plugin Instance { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;
            Exiled.Events.Handlers.Player.Hurting += Hurting;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Hurting -= Hurting;
            Instance = null;
            base.OnDisabled();
        }

        private void Hurting(HurtingEventArgs ev)
        {
            if (ev.Attacker == null) return;

            if (ev.Player.Role.Type is not RoleTypeId.Scp106) return;

            if (ev.DamageHandler.Type is not (DamageType.MicroHid or DamageType.ParticleDisruptor or DamageType.Jailbird or DamageType.Scp127))
                ev.IsAllowed = false;
        }
    }
}
