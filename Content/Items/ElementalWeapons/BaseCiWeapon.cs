using Cultivators.Content.Resources.Ci;
using Cultivators.Utility;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Cultivators.Content.Items.ElementalWeapons
{
    public class BaseCiWeapon : ModItem
    {
        protected int CiCost; // Add our custom resource cost
        protected ElementsEnum ItemElement;
        public static LocalizedText UsesXCiResourceText { get; private set; }
        public static LocalizedText RequiredCiTypeText { get; private set; }
        public override void SetStaticDefaults()
        {
            UsesXCiResourceText = Language.GetOrRegister("Mods.Cultivators.Items.BaseCiWeapon.UsesXCiResource");
            RequiredCiTypeText = Language.GetOrRegister("Mods.Cultivators.Items.BaseCiWeapon.RequiredCiType");
            ItemElement = ElementsEnum.None;
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.noMelee = true;
            Item.autoReuse = true;
            CiCost = 5; // Set our custom resource cost to 5
            ItemElement = ElementsEnum.None;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "UsesXCiResource", UsesXCiResourceText.Format(CiCost)));
            var elementTooltipText = Language.GetOrRegister($"Mods.Cultivators.Configs.ElementsConfig.{ItemElement.ToString()}");
            var elementTooltipLine = new TooltipLine(Mod, "RequiredCiType", RequiredCiTypeText.Format(elementTooltipText));
            elementTooltipLine.OverrideColor = GetTooltipElementColor();
            tooltips.Add(elementTooltipLine);
        }

        // Make sure you can't use the item if you don't have enough resource
        public override bool CanUseItem(Player player)
        {
            var exampleResourcePlayer = player.GetModPlayer<CiResourcePlayer>();
            return exampleResourcePlayer.SelectedCiElement == ItemElement &&
                exampleResourcePlayer.exampleResourceCurrent >= CiCost;
        }

        // Reduce resource on use
        public override bool? UseItem(Player player)
        {
            var exampleResourcePlayer = player.GetModPlayer<CiResourcePlayer>();
            exampleResourcePlayer.exampleResourceCurrent -= CiCost;
            return true;
        }

        private Color GetTooltipElementColor()
        {
            switch (ItemElement)
            {
                case ElementsEnum.None:
                    return new Color(255,255,255);
                case ElementsEnum.WindLightning:
                    return new Color(179, 185, 199);
                case ElementsEnum.Fire:
                    return new Color(252, 95, 4);
                case ElementsEnum.WaterIce:
                    return new Color(17, 30, 113);
                case ElementsEnum.EarthNature:
                    return new Color(56, 87, 27);
                case ElementsEnum.Death:
                    return new Color(169, 236, 203);
                case ElementsEnum.Blood:
                    return new Color(55, 23, 23);
                case ElementsEnum.Darkness:
                    return new Color(98, 24, 184);
                case ElementsEnum.Light:
                    return new Color(149, 149, 149);
                default:
                    return new Color(255, 255, 255);
            }
        }
    }
}
