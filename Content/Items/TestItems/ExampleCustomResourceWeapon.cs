﻿using Cultivators.Content.Resources.Ci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Cultivators.Content.Items.TestItems
{
    public class ExampleCustomResourceWeapon : ModItem
    {
        private int exampleResourceCost; // Add our custom resource cost

        public static LocalizedText UsesXExampleResourceText { get; private set; }

        public override void SetStaticDefaults()
        {
            UsesXExampleResourceText = this.GetLocalization("UsesXExampleResource");
        }

        public override void SetDefaults()
        {
            Item.damage = 130;
            Item.DamageType = DamageClass.Magic;
            Item.width = 38;
            Item.height = 38;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(gold: 15);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.VortexBeaterRocket;
            Item.shootSpeed = 7;
            Item.crit = 32;
            exampleResourceCost = 5; // Set our custom resource cost to 5
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "ExampleResourceCost", UsesXExampleResourceText.Format(exampleResourceCost)));
        }

        // Make sure you can't use the item if you don't have enough resource
        public override bool CanUseItem(Player player)
        {
            var exampleResourcePlayer = player.GetModPlayer<CiResourcePlayer>();

            return exampleResourcePlayer.exampleResourceCurrent >= exampleResourceCost;
        }

        // Reduce resource on use
        public override bool? UseItem(Player player)
        {
            var exampleResourcePlayer = player.GetModPlayer<CiResourcePlayer>();

            exampleResourcePlayer.exampleResourceCurrent -= exampleResourceCost;

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
