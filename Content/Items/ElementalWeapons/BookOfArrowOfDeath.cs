using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Cultivators.Content.Items.ElementalWeapons
{
    public class BookOfArrowOfDeath : BaseCiWeapon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.damage = 130;
            Item.width = 38;
            Item.height = 38;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.noMelee = true;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(gold: 15);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.VortexBeaterRocket;
            Item.shootSpeed = 7;
            Item.crit = 32;
            CiCost = 5; // Set our custom resource cost to 5
            ItemElement = Utility.ElementsEnum.Death;
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
