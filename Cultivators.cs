using Cultivators.Utility;
using System.Collections.Generic;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Cultivators
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.

    public class Cultivators : Mod
    {
        internal enum MessageType : byte
        {
            ExampleStatIncreasePlayerSync,
            ExampleTeleportToStatue,
            ExampleDodge,
            ExampleTownPetUnlockOrExchange,
            ExampleResourceEffect,
            StartVictoryPose,
            CancelVictoryPose,
        }
    }
}
