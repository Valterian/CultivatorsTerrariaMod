using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Cultivators.Utility
{
    public class ElementsConfig : ModConfig
    {
        public static LocalizedText WindLightningText { get; private set; }
        public static LocalizedText FireText { get; private set; }
        public static LocalizedText WaterIceText { get; private set; }
        public static LocalizedText EarthNatureText { get; private set; }
        public static LocalizedText DeathText { get; private set; }
        public static LocalizedText BloodText { get; private set; }
        public static LocalizedText DarknessText { get; private set; }
        public static LocalizedText LightText { get; private set; }

        public override ConfigScope Mode => ConfigScope.ServerSide;

        public override void OnLoaded()
        {
            base.OnLoaded();
            WindLightningText = this.GetLocalization("WindLightning");
            FireText = this.GetLocalization("Fire");
            WaterIceText = this.GetLocalization("WaterIce");
            EarthNatureText = this.GetLocalization("EarthNature");
            DeathText = this.GetLocalization("Death");
            BloodText = this.GetLocalization("Blood");
            DarknessText = this.GetLocalization("Darkness");
            LightText = this.GetLocalization("Light");
        }
    }
}
